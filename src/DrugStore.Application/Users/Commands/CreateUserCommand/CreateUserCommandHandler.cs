using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using FluentValidation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    : IIdempotencyCommandHandler<CreateUserCommand, Result<IdentityId>>
{
    public async Task<Result<IdentityId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => string.Equals(u.Email, request.Email, StringComparison.OrdinalIgnoreCase)))
            throw new ValidationException("Email already exists");

        ApplicationUser user = new(request.Email, request.FullName, request.Phone, request.Address);
        var result = await userManager.CreateAsync(user, request.ConfirmPassword);

        if (!result.Succeeded)
            return Result.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ));

        await userManager.AddToRoleAsync(user, Roles.Customer);

        await userManager.AddClaimsAsync(user,
        [
            new(JwtClaimTypes.Name, user.FullName!),
            new(JwtClaimTypes.Email, user.Email!),
            new(JwtClaimTypes.PhoneNumber, user.PhoneNumber!)
        ]);

        return Result<IdentityId>.Success(user.Id);
    }
}