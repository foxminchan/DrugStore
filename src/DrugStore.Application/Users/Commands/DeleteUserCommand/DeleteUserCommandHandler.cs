using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.DeleteUserCommand;

public sealed class DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);
        await userManager.DeleteAsync(user);
        return Result.Success();
    }
}