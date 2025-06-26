using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public record CreateSaleResult(
    Guid Id,
    DateTime SaleDate,
    Guid CustomerId,
    string CustomerName,
    Guid BranchId,
    string BranchName,
    List<SaleItemResult> Items,
    decimal TotalAmount,
    SaleStatus Status
);

public record SaleItemResult(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalAmount
);
