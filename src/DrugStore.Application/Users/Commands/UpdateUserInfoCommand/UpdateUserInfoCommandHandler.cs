using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.SharedKernel;
using FluentValidation;
using IdentityModel;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Users.Commands.UpdateUserInfoCommand;

public sealed class UpdateUserInfoCommandHandler(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateUserInfoCommandHandler> logger) : ICommandHandler<UpdateUserInfoCommand, Result<UserVm>>
{
    public async Task<Result<UserVm>> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        Guard.Against.NotFound(request.Id, user);

        if (!userManager.Users.Any(u => u.Email == request.Email))
        {
            logger.LogWarning("[{Command}] Email is not exists: {Email}", nameof(UpdateUserInfoCommandHandler), request.Email);
            throw new ValidationException("Email is not exists");
        }

        user.Update(request.Email, request.FullName, request.Phone, request.Address);

        logger.LogInformation("[{Command}] User information: {User}", nameof(UpdateUserCommand),
            JsonSerializer.Serialize(user));

        if (request.Role is not null) await userManager.AddToRoleAsync(user, request.Role);

        var result = await userManager.UpdateAsync(user);
        await userManager.RemoveClaimsAsync(user, await userManager.GetClaimsAsync(user));
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