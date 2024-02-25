using DrugStore.Infrastructure.Cache.Redis.Internal;
using DrugStore.Infrastructure.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Cache;

public static class Extension
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<RedisOptions>? setupAction = null)
    {
        if (services.Contains(ServiceDescriptor.Singleton<IRedisService, RedisService>()))
            return services;

        var redisOptions = new RedisOptions();
        var redisCacheSection = config.GetSection(nameof(RedisOptions));
        redisCacheSection.Bind(redisOptions);
        services.Configure<RedisOptions>(redisCacheSection);
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
