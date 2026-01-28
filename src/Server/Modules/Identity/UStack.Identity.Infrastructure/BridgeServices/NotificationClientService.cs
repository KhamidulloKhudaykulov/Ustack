using System.Net.Http.Json;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Outcome;

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
        var requestBody = new
        {
            Email = email,
            ResetToken = token
        };

        var response = await _http.PostAsJsonAsync("/api/notify/send-token", requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception($"Cannot send token: {msg}");
        }

        return Result.Success();
    }
}
