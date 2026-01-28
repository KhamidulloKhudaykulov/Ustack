using MediatR;
using UStack.Notification.Application.Dtos;
using UStack.Notification.Application.Interfaces;
using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Application.Features.SendEmail;

public class SendEmailMessageCommandHandler : IRequestHandler<SendEmailMessageCommand>
{
    private readonly INotificationSender _sender;

    public SendEmailMessageCommandHandler(INotificationSender sender)
        => _sender = sender;

    public async Task Handle(SendEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var notification = new NotificationRequest
        {
            Channel = NotificationChannelType.Email,
            Destination = request.To,
            Subject = request.Subject,
            Body = request.Body
        };

        await _sender.SendAsync(notification);
    }
}
