namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public record UpdateSaleRequest(
    Guid CustomerId,
    DateTime Date,
    Guid BranchId,
    IEnumerable<UpdateSaleItemRequest> Items
);

public record UpdateSaleItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Discount
);