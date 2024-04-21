using DrugStore.Application.Baskets.Commands.UpdateBasketCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateBasketRequest>
{
    public async Task<IResult> HandleAsync(
        UpdateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        UpdateBasketCommand command = new(request.CustomerId, request.Item);

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateBasketResponse
        {
            Basket = result.Value.Adapt<CustomerBasketDto>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/baskets", async (UpdateBasketRequest request) => await HandleAsync(request))
            .Produces<UpdateBasketResponse>()
            .WithTags(nameof(Basket))
            .WithName("Update Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}