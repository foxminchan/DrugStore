using DrugStore.Application.Products.Queries.GetByIdQuery;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetById(ISender sender) : IEndpoint<IResult, GetProductByIdRequest>
{
    public async Task<IResult> HandleAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        GetByIdQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        var response = result.Value.Adapt<ProductDto>();

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/{id}", async (ProductId id) => await HandleAsync(new(id)))
            .Produces<ProductDto>()
            .WithTags(nameof(Product))
            .WithName("Get Product By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}