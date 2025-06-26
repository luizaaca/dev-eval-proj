using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public record DeleteSaleCommand(Guid Id) : IRequest<BaseResult<DeleteSaleResult>>;