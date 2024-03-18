using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using Mapster;

namespace DrugStore.Application.Orders.Mappers;

public sealed class OrderConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Order, OrderVm>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.Total, src => src.OrderItems.Sum(x => x.Price * x.Quantity));
}