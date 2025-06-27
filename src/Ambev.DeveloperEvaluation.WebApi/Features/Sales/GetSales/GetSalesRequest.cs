namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public record GetSalesRequest(
    int PageNumber = 1,
    int PageSize = 10,
    string? OrderBy = null,
    string? Filter = null
);