namespace DrugStore.Application.Categories.ViewModels;

public sealed record NewsVm(
    Guid Id,
    string Title,
    string Detail,
    string? Image,
    Guid? CategoryId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);