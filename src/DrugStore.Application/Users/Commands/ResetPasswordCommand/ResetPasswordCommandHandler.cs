using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.ResetPasswordCommand;

public sealed class ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<ResetPasswordCommand, Result<IdentityId>>
{
    public async Task<Result<IdentityId>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(command.Id.ToString());
        Guard.Against.NotFound(command.Id, user);
        const string password = "P@ssw0rd";
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, token, password);
        return result.Succeeded
            ? Result<IdentityId>.Success(command.Id)
            : Result<IdentityId>.Invalid(new ValidationError("Reset password failed"));
    }
}