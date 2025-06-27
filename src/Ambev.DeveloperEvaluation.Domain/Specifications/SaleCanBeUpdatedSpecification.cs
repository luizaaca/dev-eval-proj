using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface ISaleCanBeUpdatedSpecification : ISpecification<Sale> { }
public class SaleCanBeUpdatedSpecification : ISaleCanBeUpdatedSpecification
{
    public bool IsSatisfiedBy(Sale sale)
    {
        return sale.Status == SaleStatus.Active;
    }
}