using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Infrastructure.Cache.Redis.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Cache;

public static class Extension
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<RedisSettings>? setupAction = null)
    {
        if (services.Contains(ServiceDescriptor.Singleton<IRedisService, RedisService>())) return services;

        RedisSettings redisSettings = new();
        var redisCacheSection = config.GetSection(nameof(RedisSettings));
        redisCacheSection.Bind(redisSettings);
        services.Configure<RedisSettings>(redisCacheSection);
        setupAction?.Invoke(redisSettings);

        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = config[redisSettings.Prefix];
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisSettings, config);
        });

        services.AddSingleton<IRedisService, RedisService>();

        return services;
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(RedisSettings redisSettings, IConfiguration config)
    {
        ConfigurationOptions configurationOptions = new()
        {
            ConnectTimeout = redisSettings.ConnectTimeout,
            SyncTimeout = redisSettings.SyncTimeout,
            ConnectRetry = redisSettings.ConnectRetry,
            AbortOnConnectFail = redisSettings.AbortOnConnectFail,
            ReconnectRetryPolicy = new ExponentialRetry(redisSettings.DeltaBackOff),
            KeepAlive = 5,
            Ssl = redisSettings.Ssl
        };

        if (!string.IsNullOrWhiteSpace(redisSettings.Password)) configurationOptions.Password = redisSettings.Password;

        redisSettings.Url = config.GetSection("RedisSettings").Get<RedisSettings>()?.Url 
                            ?? throw new InvalidOperationException();

        foreach (var endpoint in redisSettings.Url.Split(',')) configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}