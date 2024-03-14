using DrugStore.Domain.CategoryAggregate;
using DrugStore.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .HasDefaultValueSql(UniqueId.UuidAlgorithm)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(DatabaseSchemaLength.DefaultLength)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(DatabaseSchemaLength.LongLength);
    }
}