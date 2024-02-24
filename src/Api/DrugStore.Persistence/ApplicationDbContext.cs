using System.Collections.Immutable;
using DrugStore.Domain.Category;
using DrugStore.Domain.Identity;
using DrugStore.Domain.News;
using DrugStore.Domain.Order;
using DrugStore.Domain.Post;
using DrugStore.Domain.Product;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DrugStore.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IDatabaseFacade, IDomainEventContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderDetails => Set<OrderItem>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<News> News => Set<News>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();

    public IEnumerable<DomainEventBase> GetDomainEvents()
    {
        ImmutableList<EntityEntry<EntityBase>> domainEntities = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToImmutableList();

        ImmutableList<DomainEventBase> domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToImmutableList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        return domainEvents;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.DomainAssembly);
    }
}
