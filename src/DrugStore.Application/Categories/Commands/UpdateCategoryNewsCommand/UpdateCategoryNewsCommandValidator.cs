using FluentValidation;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public sealed class UpdateCategoryNewsCommandValidator : AbstractValidator<UpdateCategoryNewsCommand>
{
    public UpdateCategoryNewsCommandValidator()
    {
        RuleFor(x => x.NewsRequest.CategoryId)
            .NotEmpty();

        RuleFor(x => x.NewsRequest.NewsId)
            .NotEmpty();

        RuleFor(x => x.NewsRequest.Title)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.NewsRequest.Detail)
            .MaximumLength(500)
            .NotEmpty();

        RuleFor(x => x.ImageFile!.Length)
            .LessThanOrEqualTo(2 * 1024 * 1024)
            .WithMessage("Image size must be less than 2MB");

        RuleFor(x => x.ImageFile!.ContentType)
            .Must(x => x is "image/jpeg" or "image/png" or "image/jpg")
            .WithMessage("Image must be in jpeg or png format");

        RuleFor(x => x.NewsRequest.ImageUrl)
            .MaximumLength(100);
    }
}