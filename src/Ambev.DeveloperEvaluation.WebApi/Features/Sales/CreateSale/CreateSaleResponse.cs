namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record CreateSaleResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime Date { get; init; }
    public Guid BranchId { get; init; }
    public decimal TotalAmount { get; init; }
    public required IEnumerable<CreateSaleItemResponse> Items { get; init; }

    public CreateSaleResponse() { }

    public CreateSaleResponse(Guid id, Guid customerId, DateTime date, Guid branchId, decimal totalAmount, IEnumerable<CreateSaleItemResponse> items)
    {
        Id = id;
        CustomerId = customerId;
        Date = date;
        BranchId = branchId;
        TotalAmount = totalAmount;
        Items = items;
    }
}

public record CreateSaleItemResponse
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }

    public CreateSaleItemResponse() { }

    public CreateSaleItemResponse(Guid productId, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}