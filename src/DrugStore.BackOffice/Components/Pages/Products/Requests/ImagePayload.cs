using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class ImagePayload
{
    [AliasAs("image")] public IFormFile? File { get; set; }

    [AliasAs("alt")] public string? Alt { get; set; }
}