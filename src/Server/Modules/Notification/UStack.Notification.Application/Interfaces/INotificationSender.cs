using UStack.Notification.Application.Dtos;
using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Application.Interfaces;

public interface INotificationSender
{
    NotificationChannelType Channel { get; }
    Task SendAsync(NotificationRequest request);
}
