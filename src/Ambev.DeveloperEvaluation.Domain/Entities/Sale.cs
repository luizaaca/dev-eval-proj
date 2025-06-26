using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    public decimal TotalAmount => Items.Sum(item => item.TotalAmount);
    public SaleStatus Status { get; set; } = SaleStatus.Active;
}