using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UStack.Identity.Domain.Repositories;
using UStack.Identity.Infrastructure.Repositories;
using UStack.Identity.Application.Extensions;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Infrastructure.BridgeServices;

using System.Net.Http;

namespace UStack.Identity.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection IntegrateIdentityModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("Default"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHttpClient<IUserClient, UserClientService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7293/");
        });


        services.AddApplication();
            
        return services;
    }
}
