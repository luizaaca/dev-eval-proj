using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Sale>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Sales.Include(s => s.Items).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Sale> sales, int totalCount)> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = _context.Sales.Include(s => s.Items).AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        var sales = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (sales, totalCount);
    }

    public async Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        _context.Sales.Update(sale);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleToDelete = await _context.Sales.FindAsync([id], cancellationToken);
        if (saleToDelete != null)
        {
            _context.Sales.Remove(saleToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}