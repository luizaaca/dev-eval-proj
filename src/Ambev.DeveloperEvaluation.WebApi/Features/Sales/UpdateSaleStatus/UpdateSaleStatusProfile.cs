using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleStatus;

public class UpdateSaleStatusProfile : Profile
{
    public UpdateSaleStatusProfile()
    {
        CreateMap<UpdateSaleStatusRequest, UpdateSaleStatusCommand>();
    }
}