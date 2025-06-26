namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public record GetSalesResponse(
    int TotalCount,
    IEnumerable<GetSalesItemResponse> Items
);

public record GetSalesItemResponse(
    Guid Id,
    Guid CustomerId,
    DateTime Date,
    Guid BranchId,
    decimal TotalAmount
);