using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public record GetSaleByIdQuery(Guid Id) : IRequest<GetSaleByIdResult?>;