using FluentValidation;

namespace DrugStore.Application.Categories.Queries.GetPostListQuery;

public sealed class GetPostListQueryValidator : AbstractValidator<GetPostListQuery>
{
    public GetPostListQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(0);
    }
}
