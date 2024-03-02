using DrugStore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(
            p => p.Price,
            e => e.ToJson()
        );

        builder.OwnsMany(
            p => p.Images,
            e =>
            {
                e.Property(i => i.ImageUrl)
                    .HasMaxLength(100)
                    .IsRequired();

                e.Property(i => i.Alt)
                    .HasMaxLength(100);

                e.Property(i => i.Title)
                    .HasMaxLength(100);
            });

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(p => p.ProductCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Detail)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}