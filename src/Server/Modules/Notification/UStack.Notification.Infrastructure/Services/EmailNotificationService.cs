using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using UStack.Notification.Application.Dtos;
using UStack.Notification.Application.Interfaces;
using UStack.Notification.Domain.Enums;
using UStack.Notification.Infrastructure.Config;

namespace UStack.Notification.Infrastructure.Services;

public class EmailNotificationSender : INotificationSender
{
    private readonly SMTPSettings _smtpSettings;

    public EmailNotificationSender(IOptions<SMTPSettings> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }

    public NotificationChannelType Channel => NotificationChannelType.Email;

    public async Task SendAsync(NotificationRequest request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_smtpSettings.Username));
        email.To.Add(MailboxAddress.Parse(request.Destination));
        email.Subject = request.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = request.Body,
            TextBody = "Please use an HTML compatible email client to see this message."
        };

        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _smtpSettings.Host,
            _smtpSettings.Port
        );

        await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
