namespace DrugStore.WebAPI.Endpoints.Abstractions;

public interface IEndpointBase
{
    void MapEndpoint(IEndpointRouteBuilder app);
}