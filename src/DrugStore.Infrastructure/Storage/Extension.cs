using CloudinaryDotNet;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Infrastructure.Storage.Cloudinary.Internal;
using DrugStore.Infrastructure.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DrugStore.Infrastructure.Storage;

public static class Extension
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<CloudinarySettings>()
            .Bind(configuration.GetSection(nameof(CloudinarySettings)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        services.AddScoped<ICloudinaryUploadApi, CloudinaryDotNet.Cloudinary>(provider =>
        {
            var cloudinary = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
            return new(new Account(cloudinary.CloudName, cloudinary.ApiKey, cloudinary.ApiSecret));
        });

        services.AddScoped<ICloudinaryService, CloudinaryService>();

        return services;
    }
}