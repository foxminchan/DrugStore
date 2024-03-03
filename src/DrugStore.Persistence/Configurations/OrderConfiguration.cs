using DrugStore.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new()
            )
            .HasDefaultValueSql(UniqueHelper.UuidAlgorithm)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Code)
            .HasMaxLength(20);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(o => o.Card)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CardId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}