namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public record GetSalesResult(
    List<SaleItemDto> Items,
    int PageNumber,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

public record SaleItemDto(
    Guid Id,
    DateTime SaleDate,
    string CustomerName,
    string BranchName,
    decimal TotalAmount,
    string Status
);