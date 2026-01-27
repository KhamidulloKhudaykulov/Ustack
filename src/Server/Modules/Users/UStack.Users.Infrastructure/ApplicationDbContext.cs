using Microsoft.EntityFrameworkCore;
using UStack.Users.Infrastructure.Extensions;

namespace UStack.Users.Infrastructure;

public sealed class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
