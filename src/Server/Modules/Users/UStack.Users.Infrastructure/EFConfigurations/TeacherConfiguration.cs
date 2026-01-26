using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UStack.Users.Domain.Entities;

namespace UStack.Users.Infrastructure.EFConfigurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.IdentityUserId)
               .IsRequired();

        builder.Property(t => t.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Email)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.EmployeeNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(t => t.Department)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.HasIndex(t => t.IdentityUserId)
               .IsUnique();

        builder.HasIndex(t => t.EmployeeNumber)
               .IsUnique();
    }
}