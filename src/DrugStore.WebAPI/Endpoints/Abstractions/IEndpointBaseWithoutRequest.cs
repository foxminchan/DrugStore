namespace DrugStore.WebAPI.Endpoints.Abstractions;

public interface IEndpointBaseWithoutRequest<TResponse> : IEndpointBase
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
}