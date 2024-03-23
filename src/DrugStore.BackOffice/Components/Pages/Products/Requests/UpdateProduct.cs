using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class UpdateProduct : CreateProduct
{
    [AliasAs("id")] public Guid Id { get; set; }

    [AliasAs("imageUrl")] public string? ImageUrl { get; set; }
}