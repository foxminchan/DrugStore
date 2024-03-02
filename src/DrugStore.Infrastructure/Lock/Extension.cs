using Ardalis.GuardClauses;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DrugStore.Infrastructure.Lock;

public static class Extension
{
    public static IServiceCollection AddDistributedLock(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton<IDistributedLockProvider>(
            _ =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                Guard.Against.NullOrEmpty(connectionString);
                return new RedisDistributedSynchronizationProvider(
                    ConnectionMultiplexer.Connect(connectionString).GetDatabase()
                );
            }
        );
}