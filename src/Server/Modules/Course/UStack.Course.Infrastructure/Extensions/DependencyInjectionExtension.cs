using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UStack.Course.Application.Extensions;
using UStack.Course.Domain.Repositories;
using UStack.Course.Infrastructure.Repositories;

namespace UStack.Course.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection IntegrateCourseModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<CourseDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("Default"));
        });

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddApplication();

        return services;
    }
}
