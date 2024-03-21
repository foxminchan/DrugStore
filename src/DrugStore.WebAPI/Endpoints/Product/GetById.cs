using DrugStore.Application.Products.Queries.GetByIdQuery;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetById(ISender sender) : IEndpoint<ProductDto, GetProductByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/{id}", async (ProductId id) => await HandleAsync(new(id)))
            .Produces<ProductDto>()
            .WithTags(nameof(Product))
            .WithName("Get Product By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ProductDto> HandleAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetByIdQuery(request.Id), cancellationToken);
        return result.Value.Adapt<ProductDto>();
    }
}