using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Domain.ProductAggregate.ValueObjects;

[Owned]
public sealed class ProductImage(string imageUrl, string? alt, string? title) : ValueObject
{
    public string? ImageUrl { get; set; } = Guard.Against.NullOrEmpty(imageUrl);
    public string? Alt { get; set; } = alt;
    public string? Title { get; set; } = title;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ImageUrl ?? string.Empty;
        yield return Alt ?? string.Empty;
        yield return Title ?? string.Empty;
    }
}