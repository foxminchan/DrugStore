using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.UnitTest.Builders;

public sealed class ProductImageBuilder
{
    public static string ImageUrl => Guid.NewGuid().ToString();
    public static string Alt => "Image Alt";
    public static string Title => "Image Title";

    private readonly ProductImage _productImage = WithDefaultValues();

    public ProductImage Build() => _productImage;

    public static ProductImage WithDefaultValues() => new(ImageUrl, Alt, Title);
}