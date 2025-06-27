using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mapping;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<SaleItemCommand, SaleItem>();

        CreateMap<Sale, CreateSaleResult>();
        CreateMap<SaleItem, SaleItemResult>();

        CreateMap<Sale, GetSaleByIdResult>();
        CreateMap<SaleItem, SaleItemResultDto>();

        CreateMap<Sale, SaleDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<UpdateSaleItemDto, SaleItem>();
    }
}