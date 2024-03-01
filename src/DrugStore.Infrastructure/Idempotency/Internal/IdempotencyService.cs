using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Infrastructure.Idempotency.Abstractions;

namespace DrugStore.Infrastructure.Idempotency.Internal;

public sealed class IdempotencyService(IRedisService redisService) : IIdempotencyService
{
    public bool RequestExists(Guid id) => redisService.Get<Idempotent>(id.ToString()) is { };

    public void CreateRequestForCommand(Guid id, string name)
    {
        Idempotent idempotent = new() { Id = id, Name = name };
        redisService.GetOrSet(id.ToString(), () => idempotent, TimeSpan.FromMinutes(1));
    }
}