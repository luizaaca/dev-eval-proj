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

        // Aqui poderiam existir regras de negócio para a transição de status.
        // Ex: Uma venda 'Concluída' não pode ser 'Cancelada'.
        // if (sale.Status == SaleStatus.Completed && request.Status == SaleStatus.Canceled) return false;

        sale.Status = request.Status;

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publique o evento de atualização
        await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

        return true;
    }
}