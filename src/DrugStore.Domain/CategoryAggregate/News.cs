using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.CategoryAggregate;

public sealed class News : AuditableEntityBase
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public News()
    {
    }

    public News(string? title, string? detail, string? image, Guid? categoryId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Detail = Guard.Against.NullOrEmpty(detail);
        Image = image;
        CategoryId = categoryId;
    }

    public string? Title { get; set; }
    public string? Detail { get; set; }
    public string? Image { get; set; }
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public void Update(string? title, string? detail, string? image, Guid? categoryId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Detail = Guard.Against.NullOrEmpty(detail);
        Image = image;
        CategoryId = categoryId;
    }
}