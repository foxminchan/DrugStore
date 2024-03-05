using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Shared;

public sealed class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x.Length)
            .LessThanOrEqualTo(1048576)
            .WithMessage("File size should be less than 1MB");

        RuleFor(x => x.FileName)
            .Must(x => x.EndsWith(".jpg") || x.EndsWith(".jpeg") || x.EndsWith(".png"))
            .WithMessage("File type should be jpg, jpeg or png");
    }
}