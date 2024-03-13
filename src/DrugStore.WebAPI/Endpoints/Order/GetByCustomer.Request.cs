using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetOrderByCustomerRequest(IdentityId id, int pageIndex, int pageSize)
{
    public IdentityId Id { get; set; } = id;
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
}