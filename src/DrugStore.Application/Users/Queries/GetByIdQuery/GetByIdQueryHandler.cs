using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetByIdQuery, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);
        return Result<UserVm>.Success(mapper.Map<UserVm>(user));
    }
}