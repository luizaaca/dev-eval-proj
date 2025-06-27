
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public record GetSalesResult
{
    public int TotalCount { get; init; }
    public IEnumerable<SaleDto> Sales { get; init; } = Enumerable.Empty<SaleDto>();
    public int PageNumber { get; }
    public int PageSize { get; }

    public GetSalesResult() { }    

    public GetSalesResult(IEnumerable<SaleDto> saleDtos, int pageNumber, int pageSize, int totalCount)
    {
        Sales = saleDtos;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}

public record SaleDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime Date { get; init; }
    public Guid BranchId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string BranchName { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public required IEnumerable<SaleItemDto> Items { get; init; }

    public SaleDto() { }

    public SaleDto(
        Guid id,
        Guid customerId,
        DateTime date,
        Guid branchId,
        string customerName,
        string branchName,
        string status,
        decimal totalAmount,
        IEnumerable<SaleItemDto> items)
    {
        Id = id;
        CustomerId = customerId;
        Date = date;
        BranchId = branchId;
        CustomerName = customerName;
        BranchName = branchName;
        Status = status;
        TotalAmount = totalAmount;
        Items = items;
    }
}

public record SaleItemDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }

    public SaleItemDto() { }

    public SaleItemDto(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}