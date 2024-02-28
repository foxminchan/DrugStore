using DrugStore.Domain.ProductAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(e => e.ProductId);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Alt)
            .HasMaxLength(100);

        builder.Property(e => e.Title)
            .HasMaxLength(100);

        builder.HasOne(e => e.Product)
            .WithMany(e => e.Images)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
