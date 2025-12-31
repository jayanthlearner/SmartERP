using System.Net.Http.Headers;
using System.Net.Http.Json;

public class NotificationApi
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public NotificationApi(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AddAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);
    }

    public async Task<List<Notification>> GetMine()
    {
        AddAuthHeader();

        return await _http.GetFromJsonAsync<List<Notification>>(
            "https://localhost:5005/api/notifications") ?? [];
    }
}

public record Notification(
    Guid NotificationId,
    string Message,
    bool IsRead
);
