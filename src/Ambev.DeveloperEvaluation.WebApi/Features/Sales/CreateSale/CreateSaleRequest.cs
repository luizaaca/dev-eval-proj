namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record CreateSaleRequest(
    Guid CustomerId,
    DateTime Date,
    Guid BranchId,
    IEnumerable<CreateSaleItemRequest> Items
);

public record CreateSaleItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Discount
);