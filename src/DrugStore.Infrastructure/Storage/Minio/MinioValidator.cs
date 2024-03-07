using FluentValidation;

namespace DrugStore.Infrastructure.Storage.Minio;

public sealed class MinioValidator : AbstractValidator<MinioSettings>
{
    public MinioValidator()
    {
        RuleFor(x => x.Endpoint).NotEmpty();
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.SecretKey).NotEmpty();
    }
}