using DrugStore.Application.Baskets.Commands.DeleteBasketCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/baskets/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<Unit>()
            .WithTags(nameof(Basket))
            .WithName("Delete Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<Unit> HandleAsync(
        DeleteBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteBasketCommand(request.Id), cancellationToken);
        return Unit.Value;
    }
}