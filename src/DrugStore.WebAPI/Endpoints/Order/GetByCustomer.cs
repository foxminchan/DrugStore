using DrugStore.Application.Orders.Queries.GetListByUserIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetByCustomer(ISender sender) : IEndpoint<GetOrderByCustomerResponse, GetOrderByCustomerRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/customer/{id}", async (
                IdentityId id,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(id, pageIndex, pageSize)))
            .WithTags(nameof(Order))
            .WithName("Get Orders By Customer")
            .Produces<GetOrderByCustomerResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<GetOrderByCustomerResponse> HandleAsync(
        GetOrderByCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GetListByUserIdQuery(request.Id, new(request.PageIndex, request.PageSize)), cancellationToken
        );

        return new()
        {
            PagedInfo = result.PagedInfo,
            Orders = result.Value.Adapt<List<OrderDetailDto>>()
        };
    }
}