using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class CachedSaleRepository : ISaleRepository
{
    private readonly ISaleRepository _inner;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;

    public CachedSaleRepository(ISaleRepository inner, IMemoryCache cache, IConfiguration configuration)
    {
        _inner = inner;
        _cache = cache;

        var minutes = configuration.GetSection("Features").GetValue<int?>("SaleCacheDurationMinutes") ?? 5;
        _cacheDuration = TimeSpan.FromMinutes(minutes);
    }

    public async Task<(IEnumerable<Sale> sales, int totalCount)> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var cacheKey = $"sales_{pageNumber}_{pageSize}";
        if (_cache.TryGetValue(cacheKey, out (IEnumerable<Sale> sales, int totalCount) cached))
            return cached;

        var result = await _inner.ListAsync(pageNumber, pageSize, cancellationToken);
        _cache.Set(cacheKey, result, _cacheDuration);
        return result;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"sale_{id}";
        if (_cache.TryGetValue(cacheKey, out Sale? cachedSale))
            return cachedSale;

        var sale = await _inner.GetByIdAsync(id, cancellationToken);
        if (sale != null)
            _cache.Set(cacheKey, sale, _cacheDuration);
        return sale;
    }

    public async Task<bool> CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var result = await _inner.CreateAsync(sale, cancellationToken);
        if (result)
        {
            RemoveSalesListCache();
            _cache.Set($"sale_{sale.Id}", sale, _cacheDuration);
        }
        return result;
    }

    public async Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var result = await _inner.UpdateAsync(sale, cancellationToken);
        if (result)
        {
            _cache.Remove($"sale_{sale.Id}");
            RemoveSalesListCache();
            _cache.Set($"sale_{sale.Id}", sale, _cacheDuration);
        }
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _inner.DeleteAsync(id, cancellationToken);
        if (result)
        {
            _cache.Remove($"sale_{id}");
            RemoveSalesListCache();
        }
        return result;
    }

    private void RemoveSalesListCache()
    {
        //This is an example: if we use other cache service as Redis, 
        //we can reset by key prefix such as "sales_*"
        for (int page = 1; page <= 5; page++)
        {
            for (int size = 1; size <= 50; size += 10)
            {
                var cacheKey = $"sales_{page}_{size}";
                _cache.Remove(cacheKey);
            }
        }
    }
}