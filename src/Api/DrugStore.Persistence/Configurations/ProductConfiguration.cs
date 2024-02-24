﻿using DrugStore.Domain.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsUnicode()
            .HasColumnType("jsonb")
            .IsRequired();

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
