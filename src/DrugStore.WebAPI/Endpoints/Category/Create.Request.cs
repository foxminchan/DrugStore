namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class CreateCategoryRequest(string idempotency, CreateCategoryPayload category)
{
    public string Idempotency { get; set; } = idempotency;
    public CreateCategoryPayload Category { get; set; } = category;
}

public sealed class CreateCategoryPayload
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}