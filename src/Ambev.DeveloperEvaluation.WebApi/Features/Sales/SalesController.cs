using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        {
            return BadRequest(new ApiResponseWithData<CreateSaleResponse>
            {
                Success = false,
                Message = result.Message!,
                Data = null
            });
        }

        var response = _mapper.Map<CreateSaleResponse>(result.Data);
        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = response
        });
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
            return NotFound(new ApiResponseWithData<GetSaleByIdResponse>
            {
                Success = false,
                Message = result.Message!,
                Data = null
            });

        var response = _mapper.Map<GetSaleByIdResponse>(result.Data);
        return Ok(new ApiResponseWithData<GetSaleByIdResponse>
        {
            Success = true,
            Message = "Sale fetched successfully",
            Data = response
        });
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
            return BadRequest(new ApiResponseWithData<GetSalesResponse>
            {
                Success = false,
                Message = result.Message!,
                Data = null
            });

        var response = _mapper.Map<GetSalesResponse>(result.Data);
        return Ok(new ApiResponseWithData<GetSalesResponse>
        {
            Success = true,
            Message = "Sales fetched successfully",
            Data = response
        });
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="id">The ID of the sale to update.</param>
    /// <param name="request">The request containing the updated sale details.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
    {
        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(new ApiResponseWithData<object>
            {
                Success = false,
                Message = result.Message!,
                Data = null
            });

        return Ok(new ApiResponseWithData<object>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = null
        });
    }

    /// <summary>
    /// Deletes a sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var command = new DeleteSaleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(new ApiResponseWithData<object>
            {
                Success = false,
                Message = result.Message!,
                Data = null
            });

        return Ok(new ApiResponseWithData<object>
        {
            Success = true,
            Message = "Sale deleted successfully",
            Data = null
        });
    }
}