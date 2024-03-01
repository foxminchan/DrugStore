using FluentValidation;

namespace DrugStore.Infrastructure.Storage.Cloudinary;

public sealed class CloudinaryValidator : AbstractValidator<CloudinarySetting>
{
    public CloudinaryValidator()
    {
        RuleFor(x => x.CloudName)
            .NotEmpty()
            .WithMessage("CloudName is required");

        RuleFor(x => x.ApiKey)
            .NotEmpty()
            .WithMessage("ApiKey is required");

        RuleFor(x => x.ApiSecret)
            .NotEmpty()
            .WithMessage("ApiSecret is required");
    }
}