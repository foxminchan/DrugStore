using DrugStore.Application.Abstractions.Validators;
using FluentValidation;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed class GetListByUserIdQueryValidator : AbstractValidator<GetListByUserIdQuery>
{
    public GetListByUserIdQueryValidator(PagingFilterHelperValidator pagingFilterHelperValidator)
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Filter)
            .SetValidator(pagingFilterHelperValidator);
    }
}