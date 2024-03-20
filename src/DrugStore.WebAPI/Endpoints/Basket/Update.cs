using DrugStore.Application.Baskets.Commands.UpdateBasketCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Update(ISender sender) : IEndpoint<UpdateBasketResponse, UpdateBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/baskets", async (UpdateBasketRequest request) => await HandleAsync(request))
            .WithTags(nameof(Basket))
            .WithName("Update Basket")
            .Produces<UpdateBasketResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateBasketResponse> HandleAsync(
        UpdateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new UpdateBasketCommand(request.CustomerId, request.Item), cancellationToken);

        return new(new(
            result.Value.Id,
            [
                ..result.Value.Items.Select(x => new BasketItemDto(
                    x.ProductId,
                    x.ProductName,
                    x.Quantity,
                    x.Price,
                    x.Price * x.Quantity
                ))
            ],
            result.Value.Items.Sum(x => x.Price * x.Quantity)
        ));
    }
}