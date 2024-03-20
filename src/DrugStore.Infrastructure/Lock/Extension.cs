using DrugStore.Infrastructure.Cache.Redis;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Lock;

public static class Extension
{
    public static void AddRedisDistributedLock(this IHostApplicationBuilder builder) =>
        builder.Services.AddSingleton<IDistributedLockProvider>(
            _ => new RedisDistributedSynchronizationProvider(
                ConnectionMultiplexer.Connect(
                    builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()!.Url
                ).GetDatabase()
            ));
}