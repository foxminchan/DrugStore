using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Category;

public sealed class Category : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; }
    public string? Link { get; set; }
    public ICollection<Product.Product>? Products { get; set; } = [];
    public ICollection<Post.Post>? Posts { get; set; } = [];
    public ICollection<News.News>? News { get; set; } = [];

    /// <summary>
    /// EF mapping constructor
    /// </summary>
    public Category()
    {
    }

    public Category(string title, string? link)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Link = link;
    }

    public void Update(string title, string? link)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Link = link;
    }
}
