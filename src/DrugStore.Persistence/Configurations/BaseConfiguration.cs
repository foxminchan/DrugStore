using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableEntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(e => e.UpdateDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(e => e.Version)
            .IsConcurrencyToken();
    }
}