using DrugStore.Application.Categories.Queries.GetByIdQuery;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class GetById(ISender sender) : IEndpoint<CategoryDto, GetCategoryByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces<CategoryDto>()
            .WithTags(nameof(Category))
            .WithName("Get Category By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<CategoryDto> HandleAsync(
        GetCategoryByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetByIdQuery(request.Id), cancellationToken);
        return result.Value.Adapt<CategoryDto>();
    }
}