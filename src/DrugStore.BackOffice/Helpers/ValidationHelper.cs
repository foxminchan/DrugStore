using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Helpers;

public sealed class ValidationHelper
{
    [JsonPropertyName("validationErrors")] public List<ValidationError> ValidationErrors { get; } = [];
}

public sealed class ValidationError
{
    [JsonPropertyName("identifier")] public string Identifier { get; set; } = string.Empty;

    [JsonPropertyName("message")] public string Message { get; set; } = string.Empty;
}