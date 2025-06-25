using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events; // Adicione este using

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly IMediator _mediator; // Injete o IMediator

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger,
        IMediator mediator) // Receba o IMediator por DI
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        // Mapeia o comando para a entidade de domínio Sale
        var sale = _mapper.Map<Sale>(command);
        foreach (var itemCommand in command.Items)
        {
            var saleItem = _mapper.Map<SaleItem>(itemCommand);
            sale.Items.Add(saleItem);
        }
        sale.Id = Guid.NewGuid();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        if (createdSale)
            await _mediator.Publish(new SaleCreatedEvent(sale), cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(sale);

        return result;
    }
}