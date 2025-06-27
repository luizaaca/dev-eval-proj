using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, BaseResult<CreateSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator; // Injete o IMediator

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<BaseResult<CreateSaleResult>> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sale = _mapper.Map<Sale>(command);
            await sale.ValidateAsync();
            
            sale.Id = Guid.NewGuid();

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            if (!createdSale)
                return BaseResult<CreateSaleResult>.Fail("Failed to create the sale.");

            await _mediator.Publish(new SaleCreatedEvent(sale), cancellationToken);

            var result = _mapper.Map<CreateSaleResult>(sale);
            return BaseResult<CreateSaleResult>.Ok(result);
        }
        catch (ValidationException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            return BaseResult<CreateSaleResult>.Fail("Unexpected error while creating the sale: " + ex.Message, ex);
        }
    }
}