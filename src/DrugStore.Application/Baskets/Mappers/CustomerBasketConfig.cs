using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using Mapster;

namespace DrugStore.Application.Baskets.Mappers;

public sealed class CustomerBasketConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.ForType<CustomerBasket, CustomerBasketVm>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Items, src => src.Items.Adapt<List<BasketItemVm>>())
            .Map(dest => dest.Total, src => src.Items.Sum(x => x.Price * x.Quantity));
}