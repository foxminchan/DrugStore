using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Users.Commands.UpdateUserInfoCommand;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed class UpdateUserCommandHandler(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateUserCommandHandler> logger) : ICommandHandler<UpdateUserCommand, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);

        if (!userManager.Users.Any(u => u.Email == request.Email))
        {
            logger.LogWarning("[{Command}] Email is not exists: {Email}", nameof(UpdateUserInfoCommandHandler),
                request.Email);
            throw new ValidationException("Email is not exists");
        }

        if (!await userManager.CheckPasswordAsync(user, request.OldPassword))
        {
            logger.LogWarning("[{Command}] Old password is incorrect: {Password}", nameof(UpdateUserInfoCommandHandler),
                request.OldPassword);
            throw new ValidationException("Old password is incorrect");
        }

        user.Update(request.Email, request.FullName, request.Phone, request.Address);
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await userManager.ResetPasswordAsync(user, token, request.ConfirmPassword);

        logger.LogInformation("[{Command}] User information: {User}", nameof(UpdateUserCommand),
            JsonSerializer.Serialize(user));

        var result = await userManager.UpdateAsync(user);

        return !result.Succeeded
            ? Result<UserVm>.Invalid(new List<ValidationError>(
                result.Errors.Select(e => new ValidationError(e.Description))
            ))
            : Result<UserVm>.Success(mapper.Map<UserVm>(user));
    }
}