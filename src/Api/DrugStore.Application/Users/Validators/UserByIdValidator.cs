using DrugStore.Domain.IdentityAggregate;

using FluentValidation;

using Microsoft.AspNetCore.Identity;

namespace DrugStore.Application.Users.Validators;

public class UserByIdValidator : AbstractValidator<Guid?>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserByIdValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateId);
    }

    private async Task<bool> ValidateId(Guid? id, CancellationToken cancellation)
        => id is null || await _userManager.FindByIdAsync(id.Value.ToString()) is { };
}
