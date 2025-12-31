using System.Net.Http.Json;
using System.Text.Json;

public class AuthService
{
    private readonly HttpClient _http;

    public string? Token { get; private set; }

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync(
            "https://localhost:5001/api/auth/login",
            new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        Token = json.GetProperty("token").GetString();

        return true;
    }
}
