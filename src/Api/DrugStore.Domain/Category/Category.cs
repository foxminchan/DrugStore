using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Category;

public sealed class Category : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; }
    public string? Link { get; set; }
    public ICollection<Product.Product>? Products { get; set; } = [];
    public ICollection<Post.Post>? Posts { get; set; } = [];
    public ICollection<News.News>? News { get; set; } = [];
}
