using System.Text.Json.Serialization;
using Ardalis.GuardClauses;

namespace DrugStore.Domain.ProductAggregate;

public class ProductImage
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public ProductImage()
    {
    }

    public ProductImage(Guid productId, string imageUrl, string? alt, string? title, bool isMain)
    {
        ProductId = Guard.Against.Default(productId);
        ImageUrl = Guard.Against.NullOrEmpty(imageUrl);
        Alt = alt;
        Title = title;
        IsMain = isMain;
    }

    public Guid ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public string? Alt { get; set; }
    public string? Title { get; set; }
    public bool IsMain { get; set; }
    [JsonIgnore] public Product? Product { get; set; }
}