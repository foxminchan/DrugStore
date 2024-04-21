namespace DrugStore.Infrastructure.Endpoints;

public interface IEndpoint<TResponse, in TRequest> : IEndpointBase
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}