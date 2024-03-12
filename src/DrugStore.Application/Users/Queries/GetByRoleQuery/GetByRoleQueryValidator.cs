using DrugStore.Application.Shared;
using FluentValidation;

namespace DrugStore.Application.Users.Queries.GetByRoleQuery;

public sealed class GetByRoleQueryValidator : AbstractValidator<GetByRoleQuery>
{
    public GetByRoleQueryValidator(FilterHelperValidator filterHelperValidator)
        => RuleFor(x => x.Filter)
            .SetValidator(filterHelperValidator);
}