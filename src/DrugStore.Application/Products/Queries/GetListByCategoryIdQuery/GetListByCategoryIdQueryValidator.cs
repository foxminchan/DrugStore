using FluentValidation;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed class GetListByCategoryIdQueryValidator : AbstractValidator<GetListByCategoryIdQuery>
{
    public GetListByCategoryIdQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Filter.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThan(0);
    }
}