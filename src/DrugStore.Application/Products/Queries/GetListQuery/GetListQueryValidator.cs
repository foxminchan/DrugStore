using DrugStore.Application.Abstractions.Validators;
using FluentValidation;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed class GetListQueryValidator : AbstractValidator<GetListQuery>
{
    public GetListQueryValidator(FilterHelperValidator filterHelperValidator)
        => RuleFor(x => x.Filter)
            .SetValidator(filterHelperValidator);
}