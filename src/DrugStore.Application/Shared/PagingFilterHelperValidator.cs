using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Shared;

public sealed class PagingFilterHelperValidator : AbstractValidator<PagingHelper>
{
    public PagingFilterHelperValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0);

        RuleFor(p => p.PageSize)
            .GreaterThan(0);
    }
}