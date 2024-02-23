using Ardalis.GuardClauses;
using DrugStore.Infrastructure.Cache.Redis.Internal;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Infrastructure.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Cache;

public static class Extension
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<RedisOptions>? setupAction)
    {
        Guard.Against.Null(services, nameof(services));

        if (services.Contains(ServiceDescriptor.Singleton<IRedisService, RedisService>()))
            return services;

        services.AddOptions<RedisOptions>()
            .Bind(config.GetSection(nameof(Redis)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        var redisOptions = services.BuildServiceProvider().GetRequiredService<IOptions<RedisOptions>>().Value;
        var redisSection = config.GetSection(nameof(Redis));
        redisSection.Bind(redisOptions);
        services.Configure<RedisOptions>(redisSection);

        setupAction?.Invoke(redisOptions);

        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = config[redisOptions.Prefix];
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisOptions, config);
        });

        services.AddSingleton<IRedisService, RedisService>();

        return services;
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(RedisOptions redisOptions, IConfiguration config)
    {
        var configurationOptions = new ConfigurationOptions
        {
            ConnectTimeout = redisOptions.ConnectTimeout,
            SyncTimeout = redisOptions.SyncTimeout,
            ConnectRetry = redisOptions.ConnectRetry,
            AbortOnConnectFail = redisOptions.AbortOnConnectFail,
            ReconnectRetryPolicy = new ExponentialRetry(redisOptions.DeltaBackOff),
            KeepAlive = 5,
            Ssl = redisOptions.Ssl
        };

        if (!string.IsNullOrWhiteSpace(redisOptions.Password))
            configurationOptions.Password = redisOptions.Password;

        redisOptions.Url = config
            .GetConnectionString("Redis") ?? throw new InvalidOperationException();

        foreach (var endpoint in redisOptions.Url.Split(','))
            configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}
