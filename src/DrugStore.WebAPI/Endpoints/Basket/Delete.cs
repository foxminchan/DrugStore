using DrugStore.Application.Baskets.Commands.DeleteBasketCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteBasketRequest>
{
    public async Task<IResult> HandleAsync(
        DeleteBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteBasketCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/baskets/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Basket))
            .WithName("Delete Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}