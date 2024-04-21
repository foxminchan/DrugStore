using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class List(ISender sender) : IEndpointWithoutRequest<IResult>
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        GetListQuery query = new();

        var result = await sender.Send(query, cancellationToken);

        var response = new ListCategoryResponse
        {
            Categories = result.Value.Adapt<List<CategoryDto>>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories", HandleAsync)
            .Produces<ListCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("List Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}