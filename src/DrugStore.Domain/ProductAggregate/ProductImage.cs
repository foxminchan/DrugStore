using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate;

public class ProductImage(string imageUrl, string? alt, string? title, bool isMain) : ValueObject
{
    public string? ImageUrl { get; set; } = Guard.Against.NullOrEmpty(imageUrl);
    public string? Alt { get; set; } = alt;
    public string? Title { get; set; } = title;
    public bool IsMain { get; set; } = isMain;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ImageUrl ?? string.Empty;
        yield return Alt ?? string.Empty;
        yield return Title ?? string.Empty;
        yield return IsMain;
    }
}