using DrugStore.Application.Orders.Queries.GetListByUserIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetByCustomer(ISender sender) : IEndpoint<IResult, GetOrderByCustomerRequest>
{
    public async Task<IResult> HandleAsync(
        GetOrderByCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        GetListByUserIdQuery query = new(request.Id, new(request.PageIndex, request.PageSize));

        var result = await sender.Send(query, cancellationToken);

        var response = new GetOrderByCustomerResponse
        {
            PagedInfo = result.PagedInfo,
            Orders = result.Value.Adapt<List<OrderDetailDto>>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/customer/{id}", async (
                IdentityId id,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(id, pageIndex, pageSize)))
            .Produces<GetOrderByCustomerResponse>()
            .WithTags(nameof(Order))
            .WithName("Get Orders By Customer")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}