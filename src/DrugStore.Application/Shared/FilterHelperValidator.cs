using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Shared;

public sealed class FilterHelperValidator : AbstractValidator<FilterHelper>
{
    public FilterHelperValidator()
    {
        RuleFor(x => x.Paging.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.Paging.PageSize)
            .GreaterThan(0);
    }
}