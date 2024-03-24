using Ardalis.Result;
using DrugStore.Infrastructure.Cache.Redis;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Infrastructure.Cache;

public sealed class QueryCachingBehavior<TRequest, TResponse>(
    IRedisService redisService,
    ILogger<QueryCachingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedRequest
    where TResponse : Result<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        const string behavior = nameof(QueryCachingBehavior<TRequest, TResponse>);

        logger.LogInformation("[{Behavior}] Handling request={Request} and response={Response}",
            behavior, typeof(TRequest).FullName, typeof(TResponse).FullName);

        var response = redisService.Get<TResponse>(request.CacheKey);

        if (response is not null)
        {
            logger.LogInformation("[{Behavior}] {Request} cached query {QueryName} found", behavior, request,
                request.GetType().FullName);
            return Result<TResponse>.Success(response);
        }

        logger.LogInformation("[{Behavior}] {Request} cached query {QueryName} not found", behavior, request,
            request.GetType().FullName);
        response = await next();

        if (!response.IsSuccess)
            return response;

        logger.LogInformation("[{Behavior}] {Request} cached query {QueryName} set", behavior, request,
            request.GetType().FullName);
        redisService.GetOrSet(request.CacheKey, () => response.Value, request.CacheDuration);

        return response;
    }
}