namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public record UpdateSaleRequest(
    Guid CustomerId,
    string CustomerName,
    DateTime Date,
    Guid BranchId,
    string BranchName,
    string Status,
    IEnumerable<UpdateSaleItemRequest> Items
);

public record UpdateSaleItemRequest(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);