using DrugStore.Application.Users.Validators;
using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserRequest.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(DatabaseLengthHelper.ShortLength);

        RuleFor(x => x.UserRequest.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
            .WithMessage(
                "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, and one number");

        RuleFor(x => x.UserRequest.ConfirmPassword)
            .Equal(x => x.UserRequest.Password)
            .WithMessage("Password and Confirm Password must be the same.");

        RuleFor(x => x.UserRequest.FullName)
            .NotEmpty()
            .MaximumLength(DatabaseLengthHelper.ShortLength);

        RuleFor(x => x.UserRequest.Phone)
            .NotEmpty()
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.UserRequest.Address)
            .SetValidator(new AddressValidator()!);
    }
}