using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public record GetSaleByIdResult(
    Guid Id,
    DateTime SaleDate,
    Guid CustomerId,
    string CustomerName,
    Guid BranchId,
    string BranchName,
    List<SaleItemResultDto> Items,
    decimal TotalAmount,
    SaleStatus Status
);
