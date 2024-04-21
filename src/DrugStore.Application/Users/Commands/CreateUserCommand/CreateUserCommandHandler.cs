using System.Text.Json;
using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using FluentValidation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<CreateUserCommandHandler> logger) : IIdempotencyCommandHandler<CreateUserCommand, Result<IdentityId>>
{
    public async Task<Result<IdentityId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => u.Email == request.Email))
        {
            logger.LogWarning("[{Command}] Email already exists: {Email}", nameof(CreateUserCommand), request.Email);
            throw new ValidationException("Email already exists");
        }

        ApplicationUser user = new(request.Email, request.FullName, request.Phone, request.Address);
        var result = await userManager.CreateAsync(user, request.ConfirmPassword);

        if (!result.Succeeded)
        {
            logger.LogWarning("[{Command}] User creation failed: {Errors}", nameof(CreateUserCommand),
                JsonSerializer.Serialize(result.Errors));
            return Result.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ));
        }

        logger.LogInformation("[{Command}] User information: {User}", nameof(CreateUserCommand),
            JsonSerializer.Serialize(user));

        if (request.IsAdmin)
            await userManager.AddToRoleAsync(user, Roles.ADMIN);
        else
            await userManager.AddToRoleAsync(user, Roles.CUSTOMER);

        await userManager.AddClaimsAsync(user,
        [
            new(JwtClaimTypes.Name, user.FullName!),
            new(JwtClaimTypes.Email, user.Email!),
            new(JwtClaimTypes.PhoneNumber, user.PhoneNumber!)
        ]);

        logger.LogInformation("[{Command}] User created successfully", nameof(CreateUserCommand));

        return Result<IdentityId>.Success(user.Id);
    }
}