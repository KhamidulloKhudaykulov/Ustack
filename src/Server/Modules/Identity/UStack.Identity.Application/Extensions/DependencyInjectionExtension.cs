using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UStack.Identity.Application.UseCases.AdminActions.CreateTeacher;
using UStack.Identity.Application.UseCases.Users.CreateUser;

namespace UStack.Identity.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        services.AddScoped<CreateTeacherSaga>();

        //services.AddValidatorsFromAssemblyContaining<CreateUserValidator>()
        //    .AddFluentValidationAutoValidation();

        return services;
    }
}
