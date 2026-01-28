using MediatR;
using UStack.Notification.Application.Dtos;
using UStack.Notification.Application.Interfaces;
using UStack.Notification.Domain.Enums;

namespace UStack.Notification.Application.Features.ResetPassword;

public class ResetPasswordNotificationCommandHandler : IRequestHandler<ResetPasswordNotificationCommand>
{
    private readonly INotificationSender _sender;

    public ResetPasswordNotificationCommandHandler(INotificationSender sender)
    {
        _sender = sender;
    }

    public async Task Handle(ResetPasswordNotificationCommand request, CancellationToken cancellationToken)
    {
        var body = ResetPasswordWindow.Body
            .Replace("{{CODE}}", request.ResetToken)
            .Replace("{{EXPIRATION_MINUTES}}", "10")
            .Replace("{{YEAR}}", DateTime.UtcNow.Year.ToString());

        var notification = new NotificationRequest
        {
            Channel = NotificationChannelType.Email,
            Destination = request.Email,
            Subject = "Reset password",
            Body = body
        };

        await _sender.SendAsync(notification);
    }
}
