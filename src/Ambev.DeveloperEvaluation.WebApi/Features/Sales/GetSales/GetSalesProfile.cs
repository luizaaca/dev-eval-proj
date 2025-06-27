using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap<GetSalesResult, GetSalesResponse>();
        CreateMap<SaleDto, GetSaleResponse>();
        CreateMap<SaleItemDto, GetSaleItemResponse>();
        CreateMap<GetSalesRequest, GetSalesQuery>();
    }
}