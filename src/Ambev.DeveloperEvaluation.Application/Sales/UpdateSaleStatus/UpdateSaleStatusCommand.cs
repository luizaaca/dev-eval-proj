using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

public class UpdateSaleStatusCommand : IRequest<BaseResult<UpdateSaleStatusResult>>
{
    public Guid Id { get; set; }
    public SaleStatus Status { get; set; }
}