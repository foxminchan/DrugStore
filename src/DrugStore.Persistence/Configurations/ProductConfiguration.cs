﻿using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
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

        builder.Property(p => p.Name)
            .HasMaxLength(DatabaseSchemaLength.DefaultLength)
            .IsRequired();

        builder.OwnsOne(
            p => p.Price,
            e => e.ToJson()
        );

        builder.OwnsOne(
            p => p.Image,
            e => e.ToJson()
        );

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(p => p.ProductCode)
            .HasMaxLength(DatabaseSchemaLength.SmallLength)
            .IsRequired();

        builder.Property(p => p.Detail)
            .HasMaxLength(DatabaseSchemaLength.MaxLength)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}