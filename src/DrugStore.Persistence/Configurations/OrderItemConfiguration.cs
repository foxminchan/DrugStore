using DrugStore.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class OrderItemConfiguration : BaseConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => new { e.OrderId, e.ProductId });

        builder.Property(e => e.OrderId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ProductId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.Price)
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}