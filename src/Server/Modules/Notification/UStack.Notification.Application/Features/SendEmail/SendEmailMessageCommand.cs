using MediatR;

namespace UStack.Notification.Application.Features.SendEmail;

public record SendEmailMessageCommand(
    string To,
    string Subject,
    string Body
) : IRequest;