﻿using Ardalis.GuardClauses;

using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.CategoryAggregate;

public sealed class Category : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; }
    public string? Link { get; set; }
    public ICollection<Product>? Products { get; set; } = [];
    public ICollection<Post>? Posts { get; set; } = [];
    public ICollection<News>? News { get; set; } = [];

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
