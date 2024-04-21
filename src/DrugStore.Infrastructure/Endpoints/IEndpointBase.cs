using Microsoft.AspNetCore.Routing;

namespace DrugStore.Infrastructure.Endpoints;

public interface IEndpointBase
{
    void MapEndpoint(IEndpointRouteBuilder app);
}