using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using Mapster;

namespace DrugStore.Application.Users.Mappers;

public sealed class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<ApplicationUser, UserVm>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Phone, src => src.PhoneNumber)
            .Map(dest => dest.Address, src => src.Address);
}