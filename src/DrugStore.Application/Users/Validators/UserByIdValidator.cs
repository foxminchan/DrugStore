using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Validators;

public sealed class UserByIdValidator : AbstractValidator<IdentityId?>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserByIdValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateId);
    }

    private async Task<bool> ValidateId(IdentityId? id, CancellationToken cancellation) 
        => id is null || await _userManager.FindByIdAsync(id.Value.ToString()) is { };
}