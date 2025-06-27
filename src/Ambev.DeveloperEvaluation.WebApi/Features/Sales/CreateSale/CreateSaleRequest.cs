namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record CreateSaleRequest(
    Guid CustomerId,
    string CustomerName,
    DateTime Date,
    Guid BranchId,
    string BranchName,
    IEnumerable<CreateSaleItemRequest> Items
);

public record CreateSaleItemRequest(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);