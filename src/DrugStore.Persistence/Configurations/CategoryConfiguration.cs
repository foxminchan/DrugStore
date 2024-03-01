using DrugStore.Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Link)
            .HasMaxLength(100);
    }
}