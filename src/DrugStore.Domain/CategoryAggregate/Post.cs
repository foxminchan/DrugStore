using Ardalis.GuardClauses;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.CategoryAggregate;

public sealed class Post : AuditableEntityBase
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Post()
    {
    }

    public Post(string? title, string? detail, string? image, CategoryId? categoryId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Detail = Guard.Against.NullOrEmpty(detail);
        Image = image;
        CategoryId = categoryId;
    }

    public PostId Id { get; set; } = new(Guid.NewGuid());
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public string? Image { get; set; }
    public CategoryId? CategoryId { get; set; }
    public Category? Category { get; set; }

    public void Update(string? title, string? detail, string? image, CategoryId? categoryId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Detail = Guard.Against.NullOrEmpty(detail);
        Image = image;
        CategoryId = categoryId;
    }
}