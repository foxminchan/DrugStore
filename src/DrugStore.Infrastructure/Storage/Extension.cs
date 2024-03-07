using Ardalis.GuardClauses;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Infrastructure.Storage.Minio.Internal;
using DrugStore.Infrastructure.Validator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Minio;

namespace DrugStore.Infrastructure.Storage;

public static class Extension
{
    public static IServiceCollection AddMinioStorage(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOptions<MinioSettings>()
            .Bind(builder.Configuration.GetSection(nameof(MinioSettings)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        var minioSettings = builder.Services.BuildServiceProvider().GetService<IOptions<MinioSettings>>()?.Value;

        Guard.Against.Null(minioSettings);

        builder.Services.AddMinio(cfg => cfg
            .WithEndpoint(minioSettings.Endpoint)
            .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
        );

        builder.Services.AddScoped<IMinioService, MinioService>();

        return builder.Services;
    }
}