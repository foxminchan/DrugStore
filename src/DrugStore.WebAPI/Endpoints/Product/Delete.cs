using DrugStore.Application.Products.Commands.DeleteProductCommand;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/products/{id}", async (ProductId id) => await HandleAsync(new(id)))
            .Produces<Unit>()
            .WithTags(nameof(Product))
            .WithName("Delete Product")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();

    public async Task<Unit> HandleAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteProductCommand(request.Id), cancellationToken);
        return Unit.Value;
    }
}