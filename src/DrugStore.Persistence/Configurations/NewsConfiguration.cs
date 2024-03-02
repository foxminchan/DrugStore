using DrugStore.Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class NewsConfiguration : BaseConfiguration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
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

        builder.Property(p => p.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Detail)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Image)
            .HasMaxLength(100);

        builder.HasOne(p => p.Category)
            .WithMany(p => p.News)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}