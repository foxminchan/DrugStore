namespace DrugStore.WebAPI.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

public interface IEndpoint<TResponse> : IEndpoint
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
}

public interface IEndpoint<TResponse, in TRequest> : IEndpoint
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}