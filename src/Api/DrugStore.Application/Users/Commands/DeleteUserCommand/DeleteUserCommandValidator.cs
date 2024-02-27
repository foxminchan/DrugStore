using FluentValidation;

namespace DrugStore.Application.Users.Commands.DeleteUserCommand;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}
