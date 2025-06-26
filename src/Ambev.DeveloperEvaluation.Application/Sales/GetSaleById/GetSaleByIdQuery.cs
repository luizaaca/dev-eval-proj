using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public record GetSaleByIdQuery(Guid Id) : IRequest<BaseResult<GetSaleByIdResult>>;