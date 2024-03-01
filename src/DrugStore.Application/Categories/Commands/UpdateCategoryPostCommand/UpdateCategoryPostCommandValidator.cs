using FluentValidation;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryPostCommand;

public sealed class UpdateCategoryPostCommandValidator : AbstractValidator<UpdateCategoryPostCommand>
{
    public UpdateCategoryPostCommandValidator()
    {
        RuleFor(x => x.PostRequest.CategoryId)
            .NotEmpty();

        RuleFor(x => x.PostRequest.PostId)
            .NotEmpty();

        RuleFor(x => x.PostRequest.Title)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.PostRequest.Detail)
            .MaximumLength(500)
            .NotEmpty();

        RuleFor(x => x.ImageFile!.Length)
            .LessThanOrEqualTo(2 * 1024 * 1024)
            .WithMessage("Image size must be less than 2MB");

        RuleFor(x => x.ImageFile!.ContentType)
            .Must(x => x is "image/jpeg" or "image/png" or "image/jpg")
            .WithMessage("Image must be in jpeg or png format");
    }
}