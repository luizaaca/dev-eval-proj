using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleStatus;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The request containing the sale details.</param>
    /// <returns>The created sale details.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
    {
        var command = _mapper.Map<CreateSaleCommand>(request);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result.Message);

        var response = _mapper.Map<CreateSaleResponse>(result.Data);
        return CreatedAtAction(nameof(GetSaleById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Gets a sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale.</param>
    /// <returns>The sale details.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        var query = new GetSaleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.Success || result.Data == null)
            return NotFound(result.Message);

        var response = _mapper.Map<GetSaleByIdResponse>(result.Data);
        return Ok(response);
    }

    /// <summary>
    /// Gets a paginated list of sales.
    /// </summary>
    /// <param name="request">The request containing pagination and filtering parameters.</param>
    /// <returns>A paginated list of sales.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesRequest request)
    {
        var query = _mapper.Map<GetSalesQuery>(request);
        var result = await _mediator.Send(query);

        if (!result.Success)
            return BadRequest(result.Message);

        var response = _mapper.Map<GetSalesResponse>(result.Data);
        return Ok(response);
    }

    /// <summary>
    /// Updates the status of an existing sale.
    /// </summary>
    /// <param name="id">The ID of the sale to update.</param>
    /// <param name="request">The request containing the new status.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSaleStatus(Guid id, [FromBody] UpdateSaleStatusRequest request)
    {
        var command = _mapper.Map<UpdateSaleStatusCommand>(request);
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(result.Message);

        return NoContent();
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="id">The ID of the sale to update.</param>
    /// <param name="request">The request containing the updated sale details.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
    {
        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(result.Message);

        return NoContent();
    }

    /// <summary>
    /// Deletes a sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var command = new DeleteSaleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(result.Message);

        return NoContent();
    }
}