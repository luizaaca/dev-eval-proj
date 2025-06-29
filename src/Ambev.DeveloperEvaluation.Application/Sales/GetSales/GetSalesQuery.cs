using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesQuery : IRequest<BaseResult<GetSalesResult>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}