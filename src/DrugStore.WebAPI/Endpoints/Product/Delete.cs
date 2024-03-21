using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/products/{id}",
                async (ProductId id, bool isRemoveImage = false) => await HandleAsync(new(id, isRemoveImage)))
            .Produces<Unit>()
            .WithTags(nameof(Product))
            .WithName("Delete Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<Unit> HandleAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteProductCommand(request.Id, request.IsRemoveImage), cancellationToken);
        return Unit.Value;
    }
}