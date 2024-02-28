using DrugStore.Application.Users.Validators;
using DrugStore.Domain.IdentityAggregate.Constants;

using FluentValidation;

namespace DrugStore.Application.Users.Commands.UpdateUserCommand;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
            .WithMessage(
                "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, and one number");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Password and Confirm Password must be the same.");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(10)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Role)
            .Must(x => x is Roles.Admin or Roles.Customer)
            .WithMessage("Role must be one of Admin or Customer");

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator()!);
    }
}
