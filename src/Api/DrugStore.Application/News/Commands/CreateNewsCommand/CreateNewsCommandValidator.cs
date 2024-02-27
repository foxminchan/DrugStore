using DrugStore.Application.Categories.Validators;

using FluentValidation;

namespace DrugStore.Application.News.Commands.CreateNewsCommand;

public sealed class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
{
    public CreateNewsCommandValidator(CategoryIdValidator categoryIdValidator)
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Detail)
            .MaximumLength(500)
            .NotEmpty();

        RuleFor(x => x.ImageFile!.Length)
            .LessThanOrEqualTo(2 * 1024 * 1024)
            .WithMessage("Image size must be less than 2MB");

        RuleFor(x => x.ImageFile!.ContentType)
            .Must(x => x is "image/jpeg" or "image/png" or "image/jpg")
            .WithMessage("Image must be in jpeg or png format");

        RuleFor(x => x.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}
