using MediatR;

namespace UStack.Notification.Application.Features.ResetPassword;

public record ResetPasswordNotificationCommand(
    string Email,
    string ResetToken
) : IRequest;
