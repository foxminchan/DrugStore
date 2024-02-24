using Microsoft.AspNetCore.Routing;

namespace DrugStore.Domain.SharedKernel;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
