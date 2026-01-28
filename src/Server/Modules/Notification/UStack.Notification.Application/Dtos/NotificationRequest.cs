using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Application.Dtos;

public class NotificationRequest
{
    public NotificationChannelType Channel { get; init; }
    public string Destination { get; init; } = default!;
    public string Subject { get; init; } = default!;
    public string Body { get; init; } = default!;
}
