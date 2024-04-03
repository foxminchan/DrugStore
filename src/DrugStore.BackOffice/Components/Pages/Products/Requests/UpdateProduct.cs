namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class UpdateProduct : CreateProduct
{
    public string Id { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
}