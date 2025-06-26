using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesQuery, BaseResult<GetSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<GetSalesResult>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (sales, totalCount) = await _saleRepository.ListAsync(request.PageNumber, request.PageSize, cancellationToken);

            var saleDtos = _mapper.Map<List<SaleItemDto>>(sales);

            var result = new GetSalesResult(saleDtos, request.PageNumber, request.PageSize, totalCount);

            return BaseResult<GetSalesResult>.Ok(result);
        }
        catch (Exception ex)
        {
            return BaseResult<GetSalesResult>.Fail("Erro inesperado ao listar vendas: " + ex.Message, ex);
        }
    }
}