namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderDetailVm(
    OrderVm Order,
    List<OrderItemVm> Items
);