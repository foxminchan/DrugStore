﻿using DrugStore.Application.Users.Validators;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Users.Commands.UpdateUserInfoCommand;

public sealed class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(DatabaseSchemaLength.ShortLength);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.ShortLength);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Role)
            .Must(x => x is Roles.Admin or Roles.Customer)
            .WithMessage("Role must be one of Admin or Customer");

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator()!);
    }
}