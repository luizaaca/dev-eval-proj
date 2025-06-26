using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

public class UpdateSaleStatusHandler : IRequestHandler<UpdateSaleStatusCommand, BaseResult<UpdateSaleStatusResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    public UpdateSaleStatusHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    public async Task<BaseResult<UpdateSaleStatusResult>> Handle(UpdateSaleStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null)
                return BaseResult<UpdateSaleStatusResult>.Fail("Venda não encontrada.");

            if (!sale.CanBeUpdated())
                return BaseResult<UpdateSaleStatusResult>.Fail("Não é possível atualizar o status dessa venda.");

            sale.Status = request.Status;
            var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);

            if (!updated)
                return BaseResult<UpdateSaleStatusResult>.Fail("Falha ao atualizar o status da venda.");

            await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

            return BaseResult<UpdateSaleStatusResult>.Ok(new UpdateSaleStatusResult(true));
        }
        catch (Exception ex)
        {
            return BaseResult<UpdateSaleStatusResult>.Fail("Erro inesperado ao atualizar o status da venda: " + ex.Message, ex);
        }
    }
}