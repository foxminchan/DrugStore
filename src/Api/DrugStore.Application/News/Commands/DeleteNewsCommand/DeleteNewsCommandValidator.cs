using FluentValidation;

namespace DrugStore.Application.News.Commands.DeleteNewsCommand;

public sealed class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
{
    public DeleteNewsCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}
