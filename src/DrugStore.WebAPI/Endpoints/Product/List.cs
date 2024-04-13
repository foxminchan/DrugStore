using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class List(ISender sender) : IEndpoint<IResult, ListProductRequest>
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

    public async Task<IResult> HandleAsync(
        ListProductRequest request,
        CancellationToken cancellationToken = default)
    {
        GetListQuery query = new(request.Adapt<FilterHelper>());

        var result = await sender.Send(query, cancellationToken);

        var response = new ListProductResponse
        {
            PagedInfo = result.PagedInfo,
            Products = result.Value.Adapt<List<ProductDto>>()
        };

        return Results.Ok(response);
    }
}