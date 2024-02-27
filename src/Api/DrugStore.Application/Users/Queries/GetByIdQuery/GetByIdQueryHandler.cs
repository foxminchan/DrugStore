using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Users.ViewModel;
using DrugStore.Domain.Identity;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetByIdQuery, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);
        return Result<UserVm>.Success(new(user.Id, user.Email, user.FullName, user.Phone, user.Address));
    }
}
