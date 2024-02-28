using System.Diagnostics;

using DrugStore.Infrastructure.Idempotency.Behaviors;
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
        services.AddValidatorsFromAssemblies(AssemblyReference.AppDomainAssemblies);
        services.AddHttpContextAccessor()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddOpenBehavior(typeof(IdempotentCommandBehavior<,>));
            });
    }
}
