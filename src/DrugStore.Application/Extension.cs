using System.Diagnostics;
using DrugStore.Application.Abstractions.Behaviors;
using DrugStore.Infrastructure.Cache;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.Validator;
using DrugStore.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DrugStore.Application;

public static class Extension
{
    [DebuggerStepThrough]
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies([AssemblyReference.Executing]);
        services.AddHttpContextAccessor()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    AssemblyReference.Executing, Persistence.AssemblyReference.ExecutingAssembly
                );
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
                cfg.AddOpenBehavior(typeof(IdempotentCommandBehavior<,>));
            });
    }
}