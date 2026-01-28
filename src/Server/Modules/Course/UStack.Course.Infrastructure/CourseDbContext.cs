using Microsoft.EntityFrameworkCore;
using UStack.Course.Infrastructure.Extensions;

namespace UStack.Course.Infrastructure;

public sealed class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
