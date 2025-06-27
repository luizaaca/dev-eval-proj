using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, BaseResult<GetSaleByIdResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<GetSaleByIdResult>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                return BaseResult<GetSaleByIdResult>.Fail("Sale not found.");

            var result = _mapper.Map<GetSaleByIdResult>(sale);
            return BaseResult<GetSaleByIdResult>.Ok(result);
        }
        catch (Exception ex)
        {
            return BaseResult<GetSaleByIdResult>.Fail("Unexpected error while fetching sale: " + ex.Message, ex);
        }
    }
}