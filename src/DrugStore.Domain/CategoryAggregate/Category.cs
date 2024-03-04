using Ardalis.GuardClauses;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.CategoryAggregate;

public sealed class Category : EntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Category()
    {
    }

    public Category(string title, string? description)
    {
        Name = Guard.Against.NullOrEmpty(title);
        Description = description;
    }

    public CategoryId Id { get; set; } = new(Guid.NewGuid());
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; } = [];

    public void Update(string title, string? description)
    {
        Name = Guard.Against.NullOrEmpty(title);
        Description = description;
    }
}