using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class List(ISender sender) : IEndpointWithoutRequest<ListCategoryResponse>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories", HandleAsync)
            .Produces<ListCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("List Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ListCategoryResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetListQuery(), cancellationToken);
        return new(result.Value.Adapt<List<CategoryDto>>());
    }
}