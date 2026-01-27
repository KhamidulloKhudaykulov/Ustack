using System.Net.Http.Json;
using UStack.Identity.Application.BridgeInterfaces;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Infrastructure.BridgeServices;

internal class UserClientService : IUserClient
{
    private readonly HttpClient _http;

    public UserClientService(HttpClient http)
    {
        _http = http;
    }
    public async Task<Result<bool>> CheckExistTeacherByEmail(string email)
    {
        var response = await _http.GetAsync($"/api/users/teacher/check?email={Uri.EscapeDataString(email)}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return bool.Parse(content);
        }

        return false;
    }

    public async Task<Result<Guid>> CreateTeacher(Guid identityUserId, string firstName, string lastName, string email)
    {
        var requestBody = new
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var response = await _http.PostAsJsonAsync("/api/users/teachers", requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception($"Cannot create student: {msg}");
        }

        var studentId = await response.Content.ReadFromJsonAsync<Guid>();
        return studentId;
    }
}
