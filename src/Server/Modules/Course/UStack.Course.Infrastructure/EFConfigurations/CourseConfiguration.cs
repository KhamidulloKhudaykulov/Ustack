using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UStack.Course.Domain.Entities;

namespace UStack.Course.Infrastructure.EFConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        // Table name
        builder.ToTable("Courses", "public");

        // Primary key
        builder.HasKey(c => c.Id);

        // Columns
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(c => c.Description)
               .HasMaxLength(2000);

        builder.Property(c => c.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.TeacherIds)
            .IsRequired(false);

        builder.Property(c => c.StudentIds)
            .IsRequired(false);
    }
}
