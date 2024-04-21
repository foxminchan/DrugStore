using DrugStore.Application.Baskets.Queries.GetByUserIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class GetById(ISender sender) : IEndpoint<IResult, GetBasketByIdRequest>
{
    public async Task<IResult> HandleAsync(
        GetBasketByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        GetByUserIdQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        var response = result.Value.Adapt<CustomerBasketDto>();

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/baskets/{id}", async (IdentityId id) => await HandleAsync(new(id)))
            .Produces<CustomerBasketDto>()
            .WithTags(nameof(Basket))
            .WithName("Get Basket By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}