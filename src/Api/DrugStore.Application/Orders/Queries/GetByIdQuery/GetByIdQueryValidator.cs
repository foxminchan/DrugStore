using FluentValidation;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}
