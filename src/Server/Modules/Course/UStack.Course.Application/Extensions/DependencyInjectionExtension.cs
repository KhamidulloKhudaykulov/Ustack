using Microsoft.Extensions.DependencyInjection;

namespace UStack.Course.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        //services.AddValidatorsFromAssemblyContaining<CreateUserValidator>()
        //    .AddFluentValidationAutoValidation();

        return services;
    }
}
