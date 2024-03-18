using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using Mapster;

namespace DrugStore.Application.Orders.Mappers;

public sealed class OrderDetailConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Order, OrderDetailVm>()
            .Map(dest => dest.Order, src => src.Adapt<OrderVm>())
            .Map(dest => dest.Items, src => src.Adapt<List<OrderItem>>());
}