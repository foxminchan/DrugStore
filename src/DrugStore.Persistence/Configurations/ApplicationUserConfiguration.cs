using DrugStore.Domain.IdentityAggregate;
using DrugStore.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FullName)
            .HasMaxLength(DatabaseSchemaLength.SHORT_LENGTH)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(DatabaseSchemaLength.TINY_LENGTH);

        builder.OwnsOne(
            u => u.Address,
            a => a.ToJson()
        );
    }
}