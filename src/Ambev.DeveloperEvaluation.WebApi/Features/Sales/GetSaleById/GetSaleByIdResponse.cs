using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public record GetSaleByIdResponse(
    Guid Id,
    Guid CustomerId,
    DateTime Date,
    Guid BranchId,
    decimal TotalAmount,
    IEnumerable<GetSaleByIdItemResponse> Items
);

public record GetSaleByIdItemResponse(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal Total
);