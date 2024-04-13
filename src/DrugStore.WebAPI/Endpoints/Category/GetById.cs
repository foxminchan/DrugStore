using DrugStore.Application.Categories.Queries.GetByIdQuery;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class GetById(ISender sender) : IEndpoint<IResult, GetCategoryByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces<CategoryDto>()
            .WithTags(nameof(Category))
            .WithName("Get Category By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        GetCategoryByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        GetByIdQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        var response = result.Adapt<CategoryDto>();

        return Results.Ok(response);
    }
}