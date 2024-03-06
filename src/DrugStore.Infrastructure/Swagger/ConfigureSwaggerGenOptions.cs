using Asp.Versioning.ApiExplorer;
using DrugStore.Domain.IdentityAggregate.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DrugStore.Infrastructure.Swagger;

public sealed class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration config)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName,
                new()
                {
                    Title = $"Drug Store API {description.ApiVersion}",
                    Description =
                        "A simple drug store API",
                    Version = description.ApiVersion.ToString(),
                    Contact = new() { Name = "Nhan Nguyen", Email = "nguyenxuannhan407@gmail.com" },
                    License = new() { Name = "MIT", Url = new("https://opensource.org/licenses/MIT") }
                });

        var identityUrlExternal = config.GetValue<string>("IdentityUrlExternal");

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
            new()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    Implicit = new()
                    {
                        AuthorizationUrl = new($"{identityUrlExternal}/connect/authorize"),
                        TokenUrl = new($"{identityUrlExternal}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { ClaimHelper.Read, "Read Access to API" },
                            { ClaimHelper.Write, "Write Access to API" },
                            { ClaimHelper.Manage, "Manage Access to API" }
                        }
                    }
                }
            });

        options.AddSecurityRequirement(new()
        {
            {
                new()
                {
                    Reference = new()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>([ClaimHelper.Read, ClaimHelper.Write, ClaimHelper.Manage])
            }
        });

        options.OperationFilter<AuthorizeCheckOperationFilter>();
    }
}