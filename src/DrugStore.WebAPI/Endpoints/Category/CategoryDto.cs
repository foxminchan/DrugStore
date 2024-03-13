using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed record CategoryDto(CategoryId Id, string? Name, string? Description);