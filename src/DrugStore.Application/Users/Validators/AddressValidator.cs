using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Users.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.City)
            .MaximumLength(DatabaseSchemaLength.SHORT_LENGTH)
            .NotEmpty();

        RuleFor(a => a.Street)
            .MaximumLength(DatabaseSchemaLength.SHORT_LENGTH)
            .NotEmpty();

        RuleFor(a => a.Province)
            .MaximumLength(DatabaseSchemaLength.SHORT_LENGTH)
            .NotEmpty();
    }
}