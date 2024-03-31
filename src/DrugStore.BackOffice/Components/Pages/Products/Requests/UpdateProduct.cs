namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class UpdateProduct : CreateProduct
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; }
}