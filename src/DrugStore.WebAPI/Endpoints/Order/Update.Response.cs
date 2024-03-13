namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class UpdateOrderResponse(OrderDetailDto order)
{
    public OrderDetailDto Order { get; set; } = order;
}