using FluentValidation;

namespace DrugStore.Application.Categories.Queries.GetNewsListQuery;

public sealed class GetNewsListQueryValidator : AbstractValidator<GetNewsListQuery>
{
    public GetNewsListQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(0);
    }
}