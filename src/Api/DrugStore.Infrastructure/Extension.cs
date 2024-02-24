using DrugStore.Infrastructure.Cache;
using DrugStore.Infrastructure.Exception;
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
using System.Diagnostics;

namespace DrugStore.Infrastructure;

public static class Extension
{
    [DebuggerStepThrough]
    public static void AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.AddKestrel();
        builder.AddSerilog(builder.Environment.ApplicationName);
        builder.AddOpenTelemetry(builder.Configuration);

        services.AddVersioning();
        services.AddOpenApi();
        services.AddValidator();
        services.AddCustomProblemDetails();
        services.AddCustomExceptionHandler();

        services.AddRedisCache(builder.Configuration);
        services.AddCloudinary(builder.Configuration);
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
