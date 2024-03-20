using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class List(ISender sender) : IEndpoint<ListProductResponse, ListProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products", async (
                int pageIndex,
                int pageSize,
                string? search,
                string? orderBy,
                bool isAscending = true) => await HandleAsync(new(pageIndex, pageSize, search, orderBy, isAscending)
            ))
            .Produces<ListProductResponse>()
            .WithTags(nameof(Product))
            .WithName("List Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ListProductResponse> HandleAsync(
        ListProductRequest request,
        CancellationToken cancellationToken = default)
    {
        FilterHelper filter = new(
            request.Search,
            request.IsAscending,
            request.OrderBy,
            request.PageIndex,
            request.PageSize
        );

        var result = await sender.Send(new GetListQuery(filter), cancellationToken);

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