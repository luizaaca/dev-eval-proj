namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public record GetSalesRequest(
    int Page = 1,
    int Size = 10,
    string? OrderBy = null,
    string? Filter = null
);