using FluentValidation;

namespace DrugStore.Application.Users.Commands.ResetPasswordCommand;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator() =>
        RuleFor(x => x.Id)
            .NotEmpty();
}