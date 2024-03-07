using DrugStore.Domain.OrderAggregate;
using DrugStore.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public class CardConfiguration : BaseConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .HasDefaultValueSql(UniqueHelper.UuidAlgorithm)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Number)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(c => c.ExpiryYear)
            .IsRequired();

        builder.Property(c => c.ExpiryMonth)
            .IsRequired();

        builder.Property(c => c.Cvc)
            .IsRequired();
    }
}