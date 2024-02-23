using System.Text.Json.Serialization;

namespace DrugStore.Domain.Product;

public class ProductImage(
    Guid productId,
    string imageUrl,
    string? alt,
    string? title,
    bool isMain)
{
    public Guid ProductId { get; set; } = productId;
    public string ImageUrl { get; set; } = imageUrl;
    public string? Alt { get; set; } = alt;
    public string? Title { get; set; } = title;
    public bool IsMain { get; set; } = isMain;
    [JsonIgnore] public Product? Product { get; set; }
}
