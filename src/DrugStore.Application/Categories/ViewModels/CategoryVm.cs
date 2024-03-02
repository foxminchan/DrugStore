using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.ViewModels;

public sealed record CategoryVm(
    CategoryId Id,
    string Title,
    string? Link,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);