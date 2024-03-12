using System.ComponentModel.DataAnnotations;

namespace DrugStore.StoreFront;

public sealed class Settings
{
    [Required(ErrorMessage = "The API endpoint is required.")]
    public string ApiEndpoint { get; set; } = string.Empty;

    [Required(ErrorMessage = "The static file endpoint is required.")]
    public string StaticFileEndpoint { get; set; } = string.Empty;
}