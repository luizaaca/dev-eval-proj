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

        // Mapeamentos para o caso de uso "GetSaleById"
        CreateMap<Sale, GetSaleByIdResult>();
        CreateMap<SaleItem, SaleItemResultDto>();

        // Mapeamentos para o caso de uso "GetSales"
        CreateMap<Sale, SaleItemDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Mapeamentos para o caso de uso "UpdateSale"
        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<UpdateSaleItemDto, SaleItem>();
    }
}