using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Domain.Entities;

public class NotificationChannel
{
    public NotificationChannelType ChannelType { get; set; }
    public string? Address { get; set; }
}
