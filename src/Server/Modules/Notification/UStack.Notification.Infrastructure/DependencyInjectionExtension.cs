using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UStack.Notification.Application.Extensions;
using UStack.Notification.Application.Interfaces;
using UStack.Notification.Infrastructure.Config;
using UStack.Notification.Infrastructure.Implementations;
using UStack.Notification.Infrastructure.Services;

namespace UStack.Notification.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection IntegrateNotificationModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<INotificationSender, EmailNotificationSender>();
        services.AddScoped<INotificationService, NotificationService>();

        services.AddApplication();

        services.Configure<SMTPSettings>(config.GetSection("Smtp"));

        return services;
    }
}
