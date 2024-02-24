using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Post;

public sealed class Post : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public string? Image { get; set; }
    public Guid? CategoryId { get; set; }
    public Category.Category? Category { get; set; }

    /// <summary>
    /// EF mapping constructor
    /// </summary>
    public Post()
    {
    }

    public Post(string title, string? detail, string? image, Guid? categoryId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Detail = detail;
        Image = image;
        CategoryId = categoryId;
    }
}
