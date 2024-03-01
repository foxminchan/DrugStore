using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed class UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<UpdateUserCommand, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);

        if (userManager.Users.Any(u => u.Email == request.Email))
            return Result.Invalid(new ValidationError("Email already exists"));

        user.Update(request.Email, request.FullName, request.Phone, request.Address);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, token, request.Password);
        }

        if (request.Role is { }) await userManager.AddToRoleAsync(user, request.Role);

        var result = await userManager.UpdateAsync(user);

        return !result.Succeeded
            ? Result<UserVm>.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ))
            : Result<UserVm>.Success(new(user.Id, user.Email, user.FullName, user.PhoneNumber, user.Address));
    }
}