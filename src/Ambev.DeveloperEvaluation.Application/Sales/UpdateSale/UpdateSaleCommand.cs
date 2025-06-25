using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
public class UpdateSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public Guid BranchId { get; set; }
    public List<UpdateSaleItemDto> Items { get; set; } = new();
}

public class UpdateSaleItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public string ProductName { get; set; } = string.Empty;
}