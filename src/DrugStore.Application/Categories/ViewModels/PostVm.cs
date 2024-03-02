using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.ViewModels;

public sealed record PostVm(
    PostId Id,
    string Title,
    string Detail,
    string? Image,
    Guid? CategoryId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);