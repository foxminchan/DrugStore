using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class ListOrderResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<OrderDto>? Orders { get; set; } = [];
}