using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UStack.Identity.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("Default"));
        });

        return services;
    }
}
