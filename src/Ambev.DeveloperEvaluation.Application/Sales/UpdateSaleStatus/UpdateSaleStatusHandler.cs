using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

public class UpdateSaleStatusHandler : IRequestHandler<UpdateSaleStatusCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    public UpdateSaleStatusHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdateSaleStatusCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            return false;
        }

        if(sale.CanBeUpdated() == false)
        {
            throw new InvalidOperationException("Não é possível atualizar o status dessa venda.");
        }
        sale.Status = request.Status;

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

        return true;
    }
}