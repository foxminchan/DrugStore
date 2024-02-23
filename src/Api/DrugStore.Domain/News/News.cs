using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.News;

public sealed class News(
    string title,
    string detail,
    string? image,
    Guid categoryId) : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; } = title;
    public string? Detail { get; set; } = detail;
    public string? Image { get; set; } = image;
    public Guid CategoryId { get; set; } = categoryId;
    public Category.Category? Category { get; set; }
}
