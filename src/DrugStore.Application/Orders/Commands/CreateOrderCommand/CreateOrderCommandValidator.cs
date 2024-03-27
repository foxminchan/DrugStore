using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(IValidator<OrderItemCreateRequest> orderItemValidator)
    {
        RuleFor(x => x.Code)
            .MaximumLength(DatabaseSchemaLength.SMALL_LENGTH);

        RuleFor(x => x.Items)
            .NotEmpty()
            .ForEach(x => x.SetValidator(orderItemValidator));

        RuleFor(x => x.CustomerId)
            .NotEmpty();
    }
}

public sealed class OrderItemCreateRequestValidator : AbstractValidator<OrderItemCreateRequest>
{
    public OrderItemCreateRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}