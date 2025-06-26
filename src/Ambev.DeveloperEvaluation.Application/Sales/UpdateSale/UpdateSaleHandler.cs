using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, BaseResult<UpdateSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly ISaleCanBeUpdatedSpecification _saleCanBeUpdatedSpecification;

    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IMediator mediator,
        ISaleCanBeUpdatedSpecification saleCanBeUpdatedSpecification)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
        _saleCanBeUpdatedSpecification = saleCanBeUpdatedSpecification;
    }

    public async Task<BaseResult<UpdateSaleResult>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                return BaseResult<UpdateSaleResult>.Fail("Venda não encontrada.");

            if (!_saleCanBeUpdatedSpecification.IsSatisfiedBy(sale))
                return BaseResult<UpdateSaleResult>.Fail("Venda não pode ser atualizada.");

            sale.CustomerId = request.CustomerId;
            sale.BranchId = request.BranchId;
            sale.Items = request.Items.Select(item => new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                ProductName = item.ProductName
            }).ToList();

            var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if (!updated)
                return BaseResult<UpdateSaleResult>.Fail("Falha ao atualizar a venda.");

            await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

            return BaseResult<UpdateSaleResult>.Ok(new UpdateSaleResult(true));
        }
        catch (Exception ex)
        {
            return BaseResult<UpdateSaleResult>.Fail("Erro inesperado ao atualizar a venda: " + ex.Message, ex);
        }
    }
}