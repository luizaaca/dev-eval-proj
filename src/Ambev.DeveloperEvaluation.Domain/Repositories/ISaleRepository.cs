using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Sale>> ListAsync(CancellationToken cancellationToken);
    Task<bool> CreateAsync(Sale sale, CancellationToken cancellationToken);
    Task<(IEnumerable<Sale> sales, int totalCount)> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}