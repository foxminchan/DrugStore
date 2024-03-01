using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DrugStore.Infrastructure.Swagger;

public sealed class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
            context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ??
            context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (!hasAuthorize) return;

        operation.Responses.TryAdd("401", new() { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new() { Description = "Forbidden" });

        OpenApiSecurityScheme oAuthScheme = new()
        {
            Reference = new() { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
        };

        operation.Security =
        [
            new() { [oAuthScheme] = ["drugstore"] }
        ];
    }
}