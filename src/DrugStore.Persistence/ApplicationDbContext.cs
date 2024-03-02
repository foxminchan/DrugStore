using System.Collections.Immutable;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IDatabaseFacade, IDomainEventContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderDetails => Set<OrderItem>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<News> News => Set<News>();

    public IEnumerable<DomainEventBase> GetDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<AuditableEntityBase>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToImmutableList();

        var domainEvents = domainEntities
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