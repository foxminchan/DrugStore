using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using Mapster;

namespace DrugStore.Application.Baskets.Mappers;

public sealed class BasketItemConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.ForType<BasketItem, BasketItemVm>()
            .Map(dest => dest.ProductId, src => src.Id)
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Total, src => src.Price * src.Quantity);
}