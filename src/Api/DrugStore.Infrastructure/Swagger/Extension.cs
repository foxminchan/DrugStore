using Ardalis.GuardClauses;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DrugStore.Infrastructure.Swagger;

public static class Extension
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
        => services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
            .AddFluentValidationRulesToSwagger()
            .AddSwaggerGen();

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            Guard.Against.Null(httpReq, nameof(httpReq));

            swagger.Servers =
            [
                new()
                {
                    Url = $"{httpReq.Scheme}://{httpReq.Host.Value}",
                    Description = string.Join(" ",
                        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production,
                        "Environment")
                }
            ];
        }));

        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Rent Car API";
            c.DisplayRequestDuration();
            c.EnableFilter();
            c.EnableValidator();
            c.EnableTryItOutByDefault();
            c.EnablePersistAuthorization();
        });

        return app;
    }
}
