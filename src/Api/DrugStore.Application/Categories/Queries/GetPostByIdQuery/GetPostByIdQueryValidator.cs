using FluentValidation;

namespace DrugStore.Application.Categories.Queries.GetPostByIdQuery;

public sealed class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.PostId)
            .NotEmpty();
    }
}
