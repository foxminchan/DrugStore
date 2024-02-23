using FluentValidation;

namespace DrugStore.Infrastructure.Cache.Redis;

public sealed class RedisOptionsValidator : AbstractValidator<RedisOptions>
{
    public RedisOptionsValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty()
            .When(x => string.IsNullOrEmpty(x.GetConnectionString()));

        RuleFor(x => x.Prefix)
            .NotEmpty();
    }   
}