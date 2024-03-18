using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using Mapster;

namespace DrugStore.Application.Orders.Mappers;

public sealed class OrderItemConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<OrderItem, OrderItemVm>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Total, src => src.Quantity * src.Price);
}