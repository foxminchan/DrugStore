using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class List(ISender sender) : IEndpoint<ListProductResponse, ListProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products", async (
                string? search,
                string? orderBy,
                bool isAscending = true,
                int pageIndex = 1, 
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, search, orderBy, isAscending)
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
        var result = await sender.Send(new GetListQuery(request.Adapt<FilterHelper>()), cancellationToken);
        return new()
        {
            PagedInfo = result.PagedInfo,
            Products = result.Value.Adapt<List<ProductDto>>()
        };
    }
}