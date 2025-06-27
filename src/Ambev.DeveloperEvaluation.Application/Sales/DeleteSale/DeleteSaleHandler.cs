using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, BaseResult<DeleteSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly ISaleCanBeDeletedSpecification _saleCanBeDeletedSpecification;

    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        IMediator mediator,
        ISaleCanBeDeletedSpecification saleCanBeDeletedSpecification)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
        _saleCanBeDeletedSpecification = saleCanBeDeletedSpecification;
    }

    public async Task<BaseResult<DeleteSaleResult>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                return BaseResult<DeleteSaleResult>.Fail("Sale not found.");

            if (!_saleCanBeDeletedSpecification.IsSatisfiedBy(sale))
                return BaseResult<DeleteSaleResult>.Fail("Sale cannot be deleted.");

            var deleted = await _saleRepository.DeleteAsync(sale.Id, cancellationToken);

            if (!deleted)
                return BaseResult<DeleteSaleResult>.Fail("Failed to delete the sale.");

            await _mediator.Publish(new SaleDeletedEvent(sale.Id), cancellationToken);

            return BaseResult<DeleteSaleResult>.Ok(new DeleteSaleResult(true));
        }
        catch (Exception ex)
        {
            return BaseResult<DeleteSaleResult>.Fail("Unexpected error while deleting the sale: " + ex.Message, ex);
        }
    }
}