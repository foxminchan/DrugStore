namespace DrugStore.Infrastructure.Cache;

public interface ICachedRequest
{
    string CacheKey { get; }

    TimeSpan CacheDuration { get; }
}