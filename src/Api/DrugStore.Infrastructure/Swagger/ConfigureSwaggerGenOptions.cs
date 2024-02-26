using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DrugStore.Infrastructure.Swagger;

public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration config) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
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
        }

        options.AddSecurityDefinition("Bearer",
            new()
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

        var identityUrlExternal = config.GetValue<string>("IdentityUrlExternal");

        options.AddSecurityDefinition("oauth2", 
            new() 
            {
            Type = SecuritySchemeType.OAuth2,
            Flows = new()
            {
                Implicit = new()
                {
                    AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                    TokenUrl = new($"{identityUrlExternal}/connect/token"),
                    Scopes = new Dictionary<string, string>()
                    {
                        {
                            "read", "Read Access to API"
                        },
                        {
                            "write", "Write Access to API"
                        },
                        {
                            "manage", "Manage Access to API"
                        }
                    }
                }
            }
        });

        options.AddSecurityRequirement(new()
        {
            {
                new()
                {
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Reference = new()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });

        options.OperationFilter<AuthorizeCheckOperationFilter>();
    }
}
