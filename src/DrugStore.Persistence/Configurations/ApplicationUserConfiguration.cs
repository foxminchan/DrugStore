using DrugStore.Domain.IdentityAggregate;
using DrugStore.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FullName)
            .HasMaxLength(DatabaseLengthHelper.ShortLength)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(DatabaseLengthHelper.TinyLength);

        builder.OwnsOne(
            u => u.Address,
            a => a.ToJson()
        );
    }
}