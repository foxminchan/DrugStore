using System.Diagnostics;
using DrugStore.Infrastructure.Idempotency.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DrugStore.Infrastructure.Idempotency;

public static class Extension
{
    [DebuggerStepThrough]
    public static void AddIdempotency(this IServiceCollection services)
        => services.AddScoped<IIdempotencyService, IdempotencyService>();
}