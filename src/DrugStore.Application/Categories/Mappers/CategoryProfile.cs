using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using Mapster;

namespace DrugStore.Application.Categories.Mappers;

public sealed class CategoryProfile : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Category, CategoryVm>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);
}