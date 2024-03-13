using DrugStore.Application.Baskets.Queries.GetByUserIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class GetById(ISender sender) : IEndpoint<CustomerBasketDto, GetBasketByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/baskets/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<CustomerBasketDto>()
            .WithTags(nameof(Basket))
            .WithName("Get Basket By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<CustomerBasketDto> HandleAsync(
        GetBasketByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetByUserIdQuery(request.Id), cancellationToken);

        return new(
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
        );
    }
}