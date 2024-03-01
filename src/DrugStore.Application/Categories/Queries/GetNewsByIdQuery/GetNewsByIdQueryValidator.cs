using FluentValidation;

namespace DrugStore.Application.Categories.Queries.GetNewsByIdQuery;

public sealed class GetNewsByIdQueryValidator : AbstractValidator<GetNewsByIdQuery>
{
    public GetNewsByIdQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.NewsId)
            .NotEmpty();
    }
}