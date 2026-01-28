using MediatR;
using System.Net.Http.Json;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Outcome;
using UStack.Identity.Infrastructure.BridgeServices.Constants;

namespace UStack.Identity.Infrastructure.BridgeServices;

public class NotificationClientService : INotificationClient
{
    private readonly HttpClient _http;

    public NotificationClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result> SendEmailResetPasswordToken(string email, string token)
    {
        var body = ResetPasswordWindow.Body
            .Replace("{{CODE}}", token)
            .Replace("{{EXPIRATION_MINUTES}}", "1")
            .Replace("{{YEAR}}", DateTime.UtcNow.Year.ToString());

        var requestBody = new
        {
            To = email,
            Subject = "Reset Password",
            Body = body
        };

        var response = await _http.PostAsJsonAsync("/api/notify/send/email", requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception($"Cannot send token: {msg}");
        }

        return Result.Success();
    }
}
