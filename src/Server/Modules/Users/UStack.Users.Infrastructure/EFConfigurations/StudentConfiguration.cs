using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UStack.Users.Domain.Entities;

namespace UStack.Users.Infrastructure.EFConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.Email)
               .IsRequired()
               .HasMaxLength(200);
    }
}
