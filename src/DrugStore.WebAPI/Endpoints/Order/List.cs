using DrugStore.Application.Orders.Queries.GetListQuery;
using DrugStore.Persistence.Helpers;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class List(ISender sender) : IEndpoint<ListOrderResponse, ListOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders", async (
                string? search,
                string? orderBy,
                bool isAscending = true,
                int pageIndex = 1, 
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, search, orderBy, isAscending)))
            .WithTags(nameof(Order))
            .WithName("List Order")
            .Produces<ListOrderResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<ListOrderResponse> HandleAsync(
        ListOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetListQuery(request.Adapt<FilterHelper>()), cancellationToken);

        return new()
        {
            PagedInfo = result.PagedInfo,
            Orders = result.Value
                .Select(x => new OrderDto(x.Id, x.Code, x.Customer?.FullName, x.Customer!.Id, x.Total))
                .ToList()
        };
    }
}