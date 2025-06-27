namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public record CreateSaleResult(
    Guid Id,
    DateTime Date,
    Guid CustomerId,
    string CustomerName,
    Guid BranchId,
    string BranchName,
    List<SaleItemResult> Items,
    decimal TotalAmount,
    string Status
);

public record SaleItemResult(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalAmount
);
