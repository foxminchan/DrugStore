using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using Mapster;

namespace DrugStore.Application.Products.Mappers;

public sealed class ProductConfig : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Product, ProductVm>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ProductCode, src => src.ProductCode)
            .Map(dest => dest.Detail, src => src.Detail)
            .Map(dest => dest.Status, src => src.Status!.Name)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Category, src => src.Category!.Name)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Image, src => src.Image);
}