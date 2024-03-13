using DrugStore.Application.Orders.Queries.GetListByUserIdQuery;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetByCustomer(ISender sender) : IEndpoint<GetOrderByCustomerResponse, GetOrderByCustomerRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/customer/{id}", async (
                IdentityId id,
                int pageIndex,
                int pageSize) => await HandleAsync(new(id, pageIndex, pageSize)))
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
            new GetListByUserIdQuery(request.Id, new(request.PageIndex, request.PageSize)
            ), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Orders = result.Value.Select(
                x => new OrderDetailDto(
                    new(x.Order.Id, x.Order.Code, x.Order.Customer?.FullName, x.Order.Total),
                    [
                        ..x.Items.Select(
                            y => new OrderItemDto(y.ProductId, y.OrderId, y.Quantity, y.Price, y.Total)
                        )
                    ]
                )
            ).ToList()
        };
    }
}