using UStack.Notification.Application.Dtos;

namespace UStack.Notification.Application.Interfaces;

public interface INotificationService
{
    Task SendAsync(NotificationRequest request);
}
