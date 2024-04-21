using DrugStore.Application.Orders.Queries.GetListQuery;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using DrugStore.Persistence.Helpers;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class List(ISender sender) : IEndpoint<IResult, ListOrderRequest>
{
    public async Task<IResult> HandleAsync(
        ListOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        GetListQuery query = new(request.Adapt<FilterHelper>());

        var result = await sender.Send(query, cancellationToken);

        var orders = result.Value
            .Select(x => new OrderDto(x.Id, x.Code, x.Customer?.FullName, x.Customer!.Id, x.Total))
            .ToList();

        ListOrderResponse response = new()
        {
            PagedInfo = result.PagedInfo,
            Orders = orders
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders", async (
                string? search,
                string? orderBy,
                bool isAscending = true,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, search, orderBy, isAscending)))
            .Produces<ListOrderResponse>()
            .WithTags(nameof(Order))
            .WithName("List Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}