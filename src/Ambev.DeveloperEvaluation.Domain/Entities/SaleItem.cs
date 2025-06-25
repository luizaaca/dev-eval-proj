using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount
    {
        get
        {
            decimal discountPercentage = 0;
            if (Quantity >= 10 && Quantity <= 20)
                discountPercentage = 0.20m; // 20%
            else if (Quantity >= 4 && Quantity < 10)
                discountPercentage = 0.10m; // 10%

            return (Quantity * UnitPrice) * discountPercentage;
        }
    }
    public decimal TotalAmount => (Quantity * UnitPrice) - Discount;
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
}