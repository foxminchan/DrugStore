using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.ViewModels;

public sealed record CategoryVm(
    CategoryId Id,
    string Name,
    string? Description,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version
);