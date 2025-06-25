using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events; // Adicione este using

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator; // Injete o IMediator

    public DeleteSaleHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale is null)
        {
            return false; 
        }

        if (!sale.CanBeDeleted())
        {//mudar para domainException?
            throw new InvalidOperationException("Venda não pode ser excluída");
        }

        var deleted = await _saleRepository.DeleteAsync(sale.Id, cancellationToken);

        if (deleted)
            await _mediator.Publish(new SaleDeletedEvent(sale.Id), cancellationToken);

        return deleted;
    }
}