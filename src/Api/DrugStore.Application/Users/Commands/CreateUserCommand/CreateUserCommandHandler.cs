using Ardalis.Result;

using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.SharedKernel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => u.UserName == request.Email))
            return Result.Invalid(new ValidationError(
                nameof(request.Email),
                "Email already exists",
                StatusCodes.Status400BadRequest.ToString(),
                ValidationSeverity.Error)
            );

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.Phone,
            Address = request.Address
        };

        var result = await userManager.CreateAsync(user, request.ConfirmPassword);

        if (!result.Succeeded)
            return Result.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))));

        await userManager.AddToRoleAsync(user, Roles.Customer);

        return Result<Guid>.Success(user.Id);
    }
}
