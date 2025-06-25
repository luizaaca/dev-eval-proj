using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale is null)
            return false;

        sale.CustomerId = request.CustomerId;
        sale.BranchId = request.BranchId;

        sale.Items = [.. request.Items.Select(item => new SaleItem
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            ProductName = item.ProductName
        })];

        var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
        if (!updated)
            return false;

        await _mediator.Publish(new SaleUpdatedEvent(sale), cancellationToken);

        return true;
    }
}