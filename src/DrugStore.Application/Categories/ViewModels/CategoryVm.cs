namespace DrugStore.Application.Categories.ViewModels;

public sealed record CategoryVm(
    Guid Id,
    string Title,
    string? Link,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);