using DrugStore.Application.Users.Validators;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(DatabaseSchemaLength.SHORT_LENGTH);

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
            .MaximumLength(DatabaseSchemaLength.SHORT_LENGTH);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator()!);
    }
}