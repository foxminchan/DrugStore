﻿using FluentValidation;

namespace DrugStore.Application.Categories.Queries.GetByIdQuery;

public sealed class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}