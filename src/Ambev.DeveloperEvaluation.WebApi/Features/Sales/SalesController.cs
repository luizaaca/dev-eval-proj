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
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
    {
        var command = _mapper.Map<CreateSaleCommand>(request);
        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = result.Message!
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
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        var query = new GetSaleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.Success || result.Data == null)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = result.Message!
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
    [ProducesResponseType(typeof(ApiResponseWithData<GetSalesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesRequest request)
    {
        var query = _mapper.Map<GetSalesQuery>(request);
        var result = await _mediator.Send(query);

        if (!result.Success)
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = result.Message!
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
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
    {
        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = result.Message!
            });

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale updated successfully",
        });
    }

    /// <summary>
    /// Deletes a sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>No content if successful, or not found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var command = new DeleteSaleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = result.Message!
            });

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }
}