﻿using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure;
using Ambev.DeveloperEvaluation.Infrastructure.Events;
using Ambev.DeveloperEvaluation.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddMemoryCache();

        var enableCache = builder.Configuration.GetSection("Features").GetValue<bool>("EnableSaleCache");

        if (enableCache)
        {
            builder.Services.AddScoped<SaleRepository>();
            builder.Services.AddScoped<ISaleRepository>(sp =>
                new CachedSaleRepository(sp.GetRequiredService<SaleRepository>(), sp.GetRequiredService<IMemoryCache>(), sp.GetRequiredService<IConfiguration>())
            );
        }
        else
        {
            builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        }

        builder.Services.AddScoped<IEventPublisher, ConsolePublisher>();
    }
}