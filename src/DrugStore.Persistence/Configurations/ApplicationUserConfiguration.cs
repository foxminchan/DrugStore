using DrugStore.Domain.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FullName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(10);

        builder.OwnsOne(
            u => u.Address,
            a => a.ToJson()
        );
    }
}