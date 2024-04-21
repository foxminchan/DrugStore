using DrugStore.Application.Shared;
using FluentValidation;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed class GetListByCategoryIdQueryValidator : AbstractValidator<GetListByCategoryIdQuery>
{
    public GetListByCategoryIdQueryValidator(PagingFilterHelperValidator pagingFilterHelperValidator)
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Filter)
            .SetValidator(pagingFilterHelperValidator);
    }
}