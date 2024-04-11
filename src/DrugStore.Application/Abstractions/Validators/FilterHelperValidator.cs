using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Abstractions.Validators;

public sealed class FilterHelperValidator : AbstractValidator<FilterHelper>
{
    public FilterHelperValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}