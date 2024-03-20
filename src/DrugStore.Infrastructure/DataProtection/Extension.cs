using DrugStore.Infrastructure.Cache.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.DataProtection;

public static class Extension
{
    public static void AddRedisDataProtection(this IHostApplicationBuilder builder) =>
        builder.Services.AddDataProtection()
            .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
            .SetApplicationName(builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()!.Prefix)
            .PersistKeysToStackExchangeRedis(
                ConnectionMultiplexer.Connect(
                    builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()!.Url
                ), "DataProtectionKeys"
            );
}