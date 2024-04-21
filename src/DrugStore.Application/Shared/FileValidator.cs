using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Shared;

public sealed class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x.Length)
            .LessThanOrEqualTo(int.MaxValue)
            .WithMessage("File size should be less than 2MB");

        RuleFor(x => x.FileName)
            .Must(x => x.EndsWith(".jpg") || x.EndsWith(".jpeg") || x.EndsWith(".png"))
            .WithMessage("File type should be jpg, jpeg or png");
    }
}