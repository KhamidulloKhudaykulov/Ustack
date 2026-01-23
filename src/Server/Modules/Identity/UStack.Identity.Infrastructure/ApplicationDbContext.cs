using Microsoft.EntityFrameworkCore;

namespace UStack.Identity.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
