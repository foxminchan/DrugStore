using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class List(ISender sender) : IEndpointWithoutRequest<IResult>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories", HandleAsync)
            .Produces<ListCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("List Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetListQuery();

        var result = await sender.Send(query, cancellationToken);

        var response = new ListCategoryResponse
        {
            Categories = result.Value.Adapt<List<CategoryDto>>()
        };

        return Results.Ok(response);
    }
}