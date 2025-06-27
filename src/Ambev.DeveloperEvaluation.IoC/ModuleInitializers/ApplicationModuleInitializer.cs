using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {        
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Logging.LoggingBehavior<,>));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Validation.ValidationBehavior<,>));
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
    }
}