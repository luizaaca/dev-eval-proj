using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
                return BaseResult<UpdateSaleResult>.Fail("Sale not found.");

            if (!_saleCanBeUpdatedSpecification.IsSatisfiedBy(sale))
                return BaseResult<UpdateSaleResult>.Fail("Sale cannot be updated.");

            sale.CustomerId = request.CustomerId;
            sale.BranchId = request.BranchId;

            if (!Enum.TryParse<SaleStatus>(request.Status, out var saleStatus))
                return BaseResult<UpdateSaleResult>.Fail("Invalid sale status.");
            sale.Status = saleStatus;

            sale.Items = request.Items.Select(item => new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                ProductName = item.ProductName
            }).ToList();

            await sale.ValidateAsync();

            var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if (!updated)
                return BaseResult<UpdateSaleResult>.Fail("Failed to update the sale.");

            await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

            return BaseResult<UpdateSaleResult>.Ok(new UpdateSaleResult(true));
        }
        catch (ValidationException ex) { throw ex; }
        catch (Exception ex)
        {
            return BaseResult<UpdateSaleResult>.Fail("Unexpected error while updating the sale: " + ex.Message, ex);
        }
    }
}