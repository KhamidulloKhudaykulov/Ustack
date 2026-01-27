using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UStack.Users.Domain.Repositories;
using UStack.Users.Infrastructure.Repositories;
using UStack.Users.Application.Extensions;

namespace UStack.Users.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection IntegrateUsersModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("Default"));
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddApplication();

        return services;
    }
}
