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
        logger.LogInformation("{Request} handled query {QueryName}", request, request.GetType().Name);

        var response = redisService.Get<TResponse>(request.CacheKey);

        if (response is { })
        {
            logger.LogInformation("{Request} cached query {QueryName} found", request, request.GetType().Name);
            return Result<TResponse>.Success(response);
        }

        logger.LogInformation("{Request} cached query {QueryName} not found", request, request.GetType().Name);

        response = await next();

        if (!response.IsSuccess) 
            return response;

        logger.LogInformation("{Request} cached query {QueryName} set", request, request.GetType().Name);
        redisService.GetOrSet(request.CacheKey, () => response.Value, request.CacheDuration);

        return response;
    }
}