using DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetByCategory(ISender sender) : IEndpoint<GetProductByCategoryResponse, GetProductByCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/category/{id}", async (
                CategoryId id,
                int pageIndex,
                int pageSize) => await HandleAsync(new(id, pageIndex, pageSize)
            ))
            .Produces<GetProductByCategoryResponse>()
            .WithTags(nameof(Product))
            .WithName("Get Product By Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<GetProductByCategoryResponse> HandleAsync(
        GetProductByCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        PagingHelper filter = new(request.PageIndex, request.PageSize);

        var result = await sender.Send(new GetListByCategoryIdQuery(request.Id, filter), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Products =
            [
                ..result.Value.Select(x => new ProductDto(
                    x.Id,
                    x.Name,
                    x.ProductCode,
                    x.Detail,
                    x.Status?.Name,
                    x.Quantity,
                    x.Category?.Name,
                    x.Price,
                    x.Image
                ))
            ]
        };
    }
}