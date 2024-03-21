using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Update(ISender sender) : IEndpoint<UpdateProductResponse, UpdateProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/products", async (UpdateProductRequest request) => await HandleAsync(request))
            .Produces<UpdateProductResponse>()
            .WithTags(nameof(Product))
            .WithName("Update Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateProductResponse> HandleAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(request.Adapt<UpdateProductCommand>(), cancellationToken);
        return new(result.Value.Adapt<ProductDto>());
    }
}