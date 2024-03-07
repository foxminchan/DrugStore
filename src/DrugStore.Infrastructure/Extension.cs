using System.Diagnostics;
using DrugStore.Infrastructure.Cache;
using DrugStore.Infrastructure.Exception;
using DrugStore.Infrastructure.Idempotency;
using DrugStore.Infrastructure.Kestrel;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using DrugStore.Infrastructure.ProblemDetails;
using DrugStore.Infrastructure.Storage;
using DrugStore.Infrastructure.Swagger;
using DrugStore.Infrastructure.Validator;
using DrugStore.Infrastructure.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DrugStore.Infrastructure;

public static class Extension
{
    [DebuggerStepThrough]
    public static void AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddValidator();
        services.AddVersioning();
        services.AddOpenApi();
        services.AddIdempotency();
        services.AddCustomProblemDetails();
        services.AddCustomExceptionHandler();

        builder.AddKestrel();
        builder.AddRedisCache();
        builder.AddMinioStorage();
        builder.AddOpenTelemetry();
        builder.AddSerilog(builder.Environment.ApplicationName);
    }

    [DebuggerStepThrough]
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseKestrel();
        app.UseOpenApi();
        app.UseCustomProblemDetails();
        app.UseCustomExceptionHandler();
    }
}