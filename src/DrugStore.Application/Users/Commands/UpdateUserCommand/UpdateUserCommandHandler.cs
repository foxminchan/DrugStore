using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;
using FluentValidation;
using IdentityModel;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed class UpdateUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    : ICommandHandler<UpdateUserCommand, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);

        if (userManager.Users.Any(u => u.Email == request.Email))
            throw new ValidationException("Email already exists");

        user.Update(request.Email, request.FullName, request.Phone, request.Address);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, token, request.Password);
        }

        if (request.Role is not null) await userManager.AddToRoleAsync(user, request.Role);

        var result = await userManager.UpdateAsync(user);

        var claims = await userManager.GetClaimsAsync(user);
        await userManager.RemoveClaimsAsync(user, claims);
        await userManager.AddClaimsAsync(user,
        [
            new(JwtClaimTypes.Name, user.FullName!),
            new(JwtClaimTypes.Email, user.Email!),
            new(JwtClaimTypes.PhoneNumber, user.PhoneNumber!)
        ]);

        return !result.Succeeded
            ? Result<UserVm>.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ))
            : Result<UserVm>.Success(mapper.Map<UserVm>(user));
    }
}