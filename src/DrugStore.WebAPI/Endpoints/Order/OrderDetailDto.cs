namespace DrugStore.WebAPI.Endpoints.Order;

public sealed record OrderDetailDto(
    OrderDto Order,
    List<OrderItemDto> Items
);