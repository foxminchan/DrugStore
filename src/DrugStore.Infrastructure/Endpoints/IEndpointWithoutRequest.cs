namespace DrugStore.Infrastructure.Endpoints;

public interface IEndpointWithoutRequest<TResponse> : IEndpointBase
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
}