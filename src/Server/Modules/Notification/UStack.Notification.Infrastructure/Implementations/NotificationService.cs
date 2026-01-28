using UStack.Notification.Application.Dtos;
using UStack.Notification.Application.Interfaces;
using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Infrastructure.Implementations;

public class NotificationService : INotificationService
{
    private readonly Dictionary<NotificationChannelType, INotificationSender> _senders;

    public NotificationService(IEnumerable<INotificationSender> senders)
    {
        _senders = senders.ToDictionary(s => s.Channel);
    }

    public async Task SendAsync(NotificationRequest request)
    {
        if (!_senders.TryGetValue(request.Channel, out var sender))
            throw new InvalidOperationException($"No sender registered for channel {request.Channel}");

        await sender.SendAsync(request);
    }
}
