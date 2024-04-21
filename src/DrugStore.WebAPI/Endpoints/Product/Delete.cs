using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteProductRequest>
{
    public async Task<IResult> HandleAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteProductCommand command = new(request.Id, request.IsRemoveImage);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/products/{id}",
                async (ProductId id, bool isRemoveImage = false) => await HandleAsync(new(id, isRemoveImage)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Product))
            .WithName("Delete Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}