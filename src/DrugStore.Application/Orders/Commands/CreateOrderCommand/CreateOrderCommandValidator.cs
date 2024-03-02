using DrugStore.Application.Products.Validators;
using DrugStore.Application.Users.Validators;
using FluentValidation;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(
        UserByIdValidator userByIdValidator,
        IValidator<OrderItemCreateRequest> orderItemValidator)
    {
        RuleFor(x => x.Request.Code)
            .MaximumLength(20);

        RuleFor(x => x.Request.Status)
            .IsInEnum();

        RuleFor(x => x.Request.PaymentMethod)
            .IsInEnum();

        RuleFor(x => x.Request.Items)
            .NotEmpty()
            .ForEach(x => x.SetValidator(orderItemValidator));

        RuleFor(x => x.Request.CustomerId)
            .NotEmpty()
            .SetValidator(userByIdValidator);
    }
}

public sealed class OrderItemCreateRequestValidator : AbstractValidator<OrderItemCreateRequest>
{
    public OrderItemCreateRequestValidator(ProductIdValidator productIdValidator)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .SetValidator(productIdValidator);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}