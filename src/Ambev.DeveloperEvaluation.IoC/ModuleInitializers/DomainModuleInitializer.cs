using Ambev.DeveloperEvaluation.Domain.Specifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ISaleCanBeUpdatedSpecification, SaleCanBeUpdatedSpecification>();
        builder.Services.AddSingleton<ISaleCanBeDeletedSpecification, SaleCanBeDeletedSpecification>();
    }
}