namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public record GetSalesResponse
{
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public required IEnumerable<GetSaleResponse> Sales { get; init; }

    public GetSalesResponse() { }

    public GetSalesResponse(int totalCount, IEnumerable<GetSaleResponse> sales, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        Sales = sales;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public record GetSaleResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime Date { get; init; }
    public Guid BranchId { get; init; }
    public decimal TotalAmount { get; init; }
    public required string CustomerName { get; init; }
    public required string BranchName { get; init; }
    public required string Status { get; init; }
    public required IEnumerable<GetSaleItemResponse> Items { get; init; }

    public GetSaleResponse() { }

    public GetSaleResponse(Guid id, Guid customerId, DateTime date, Guid branchId, decimal totalAmount, IEnumerable<GetSaleItemResponse> items)
    {
        Id = id;
        CustomerId = customerId;
        Date = date;
        BranchId = branchId;
        TotalAmount = totalAmount;
        Items = items;
    }
}

public record GetSaleItemResponse
{
    public Guid ProductId { get; init; }
    public required string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }
    public GetSaleItemResponse() { }
    public GetSaleItemResponse(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}