namespace DrugStore.Application.News.ViewModel;

public sealed record NewsVm(
    Guid Id,
    string Title,
    string Detail,
    string? Image,
    Guid? CategoryId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);
