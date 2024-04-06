using Asp.Versioning.ApiExplorer;
using DrugStore.Domain.IdentityAggregate.Constants;
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
        {
            options.SwaggerDoc(description.GroupName,
                new()
                {
                    Title = $"Drug Store API {description.ApiVersion}",
                    Description = "A simple drug store application with Clean Architecture, DDD, CQRS, REPR",
                    Version = description.ApiVersion.ToString(),
                    Contact = new() { Name = "Nhan Nguyen", Email = "nguyenxuannhan407@gmail.com" },
                    License = new() { Name = "MIT", Url = new("https://opensource.org/licenses/MIT") }
                });
        }

        var urlExternal = config.GetValue<string>("IdentityServer:Authority");

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
            new()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    AuthorizationCode = new()
                    {
                        TokenUrl = new($"{urlExternal}/connect/token"),
                        AuthorizationUrl = new($"{urlExternal}/connect/authorize"),
                        Scopes = new Dictionary<string, string>
                        {
                            { Claims.READ, "Read Access to API" },
                            { Claims.WRITE, "Write Access to API" },
                            { Claims.MANAGE, "Manage Access to API" }
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
                new List<string>([Claims.READ, Claims.WRITE, Claims.MANAGE])
            }
        });

        options.OperationFilter<AuthorizeCheckOperationFilter>();
    }
}