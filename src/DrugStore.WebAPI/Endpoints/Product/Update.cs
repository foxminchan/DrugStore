using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
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
        var result = await sender.Send(
            new UpdateProductCommand(
                request.Id,
                request.Name,
                request.ProductCode,
                request.Detail,
                request.Quantity,
                request.CategoryId,
                request.ProductPrice,
                request.ImageUrl
            ), cancellationToken
        );

        var product = result.Value;

        return new(new(
            product.Id,
            product.Name,
            product.ProductCode,
            product.Detail,
            product.Status?.Name,
            product.Quantity,
            product.Category?.Name,
            product.Price,
            product.Image
        ));
    }
}