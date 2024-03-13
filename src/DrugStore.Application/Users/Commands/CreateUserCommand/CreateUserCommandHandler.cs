using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Helpers;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    : IIdempotencyCommandHandler<CreateUserCommand, Result<IdentityId>>
{
    public async Task<Result<IdentityId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => string.Equals(
                u.Email, request.Email, StringComparison.OrdinalIgnoreCase
            )))
            return Result.Invalid(new ValidationError(
                nameof(request.Email),
                "Email already exists",
                StatusCodes.Status400BadRequest.ToString(),
                ValidationSeverity.Error
            ));

        ApplicationUser user = new(
            request.Email,
            request.FullName,
            request.Phone,
            request.Address
        );

        var result = await userManager.CreateAsync(user, request.ConfirmPassword);

        if (!result.Succeeded)
            return Result.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ));

        await userManager.AddToRoleAsync(user, RoleHelper.Customer);

        return Result<IdentityId>.Success(user.Id);
    }
}