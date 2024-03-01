using FluentValidation;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed class GetListQueryValidator : AbstractValidator<GetListQuery>
{
    public GetListQueryValidator()
    {
        RuleFor(x => x.Filter.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.Filter.PageSize)
            .GreaterThan(0);
    }
}