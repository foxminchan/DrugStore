using DrugStore.Application.Products.Commands.UpdateProductImageCommand;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class UpdateImage(ISender sender) : IEndpoint<UpdateProductImageResponse, UpdateProductImageRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/products/image/{id}", async (
                ProductId id,
                [FromForm] IFormFile image,
                [FromForm] string alt) => await HandleAsync(new(id, image, alt)))
            .Produces<UpdateProductImageResponse>()
            .WithTags(nameof(Product))
            .WithName("Update Product Image")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit()
            .DisableAntiforgery();

    public async Task<UpdateProductImageResponse> HandleAsync(
        UpdateProductImageRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new UpdateProductImageCommand(request.Id, request.Alt, request.Image), cancellationToken
        );

        return new(result.Value);
    }
}