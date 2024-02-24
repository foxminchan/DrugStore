using DrugStore.Domain.Identity;
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

        builder.Property(u => u.Phone)
            .HasMaxLength(10);

        builder.Property(u => u.Address)
            .IsUnicode()
            .HasColumnType("jsonb");
    }
}
