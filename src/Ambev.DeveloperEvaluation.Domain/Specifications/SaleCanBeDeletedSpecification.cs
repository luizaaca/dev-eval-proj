using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface ISaleCanBeDeletedSpecification : ISpecification<Sale> { }
public class SaleCanBeDeletedSpecification : ISaleCanBeDeletedSpecification
{
    public bool IsSatisfiedBy(Sale sale)
    {
        return sale.Status == SaleStatus.Cancelled;
    }
}