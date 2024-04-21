using DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetByCategory(ISender sender) : IEndpoint<IResult, GetProductByCategoryRequest>
{
    public async Task<IResult> HandleAsync(
        GetProductByCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        GetListByCategoryIdQuery query = new(request.Id, new(request.PageIndex, request.PageSize));

        var result = await sender.Send(query, cancellationToken);

        var response = new GetProductByCategoryResponse
        {
            PagedInfo = result.PagedInfo,
            Products = result.Value.Adapt<List<ProductDto>>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/category/{id}", async (
                CategoryId id,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(id, pageIndex, pageSize)
            ))
            .Produces<GetProductByCategoryResponse>()
            .WithTags(nameof(Product))
            .WithName("Get Product By Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}