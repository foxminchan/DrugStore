using DrugStore.Domain.IdentityAggregate.ValueObjects;
using FluentValidation;

namespace DrugStore.Application.Users.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.City)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(a => a.Street)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(a => a.Province)
            .MaximumLength(50)
            .NotEmpty();
    }
}