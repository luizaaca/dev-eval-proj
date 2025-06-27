namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public record GetSaleByIdResult(
    Guid Id,
    DateTime Date,
    Guid CustomerId,
    string CustomerName,
    Guid BranchId,
    string BranchName,
    List<SaleItemResultDto> Items,
    decimal TotalAmount,
    string Status
);
