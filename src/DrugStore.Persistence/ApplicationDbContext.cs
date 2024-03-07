using System.Collections.Immutable;
using System.Reflection;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartEnum.EFCore;

namespace DrugStore.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, IdentityId>(options), IDatabaseFacade, IDomainEventContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderDetails => Set<OrderItem>();
    public DbSet<Card> Cards => Set<Card>();

    public IEnumerable<DomainEventBase> GetDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<EntityBase>()
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
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            ).Where(prop => prop.PropertyType == typeof(IdentityId));

            foreach (var prop in properties)
                builder.Entity(entityType.Name).Property(prop.Name)
                    .HasConversion(new ValueConverter<IdentityId, Guid>(
                        id => id.Value,
                        value => new(value)
                    ))
                    .HasDefaultValueSql(UniqueHelper.UuidAlgorithm);
        }

        base.OnModelCreating(builder);
        builder.ConfigureSmartEnum();
        builder.HasPostgresExtension(UniqueHelper.UuidExtension);
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.DomainAssembly);
    }
}