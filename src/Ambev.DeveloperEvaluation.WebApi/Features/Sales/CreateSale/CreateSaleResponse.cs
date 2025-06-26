namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record CreateSaleResponse(
    Guid Id,
    Guid CustomerId,
    DateTime Date,
    Guid BranchId,
    decimal TotalAmount,
    IEnumerable<CreateSaleItemResponse> Items
);

public record CreateSaleItemResponse(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal Total
);