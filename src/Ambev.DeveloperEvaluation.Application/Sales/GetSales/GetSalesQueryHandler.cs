using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, GetSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<GetSalesResult> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        // Assumindo que o repositório tem um método para listagem paginada.
        // Precisaremos definir isso na camada de Domínio se não existir.
        var (sales, totalCount) = await _saleRepository.ListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var saleDtos = _mapper.Map<List<SaleSummaryDto>>(sales);

        return new GetSalesResult
        {
            Items = saleDtos,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}