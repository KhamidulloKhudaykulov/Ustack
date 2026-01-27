using Microsoft.Extensions.DependencyInjection;

namespace UStack.Users.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
