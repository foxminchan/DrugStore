using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetOrderByCustomerResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<OrderDetailDto>? Orders { get; set; } = [];
}