using System.Net.Http.Headers;
using System.Net.Http.Json;

public class EmployeeApi
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public EmployeeApi(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AddAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);
    }

    public async Task<List<Employee>> GetAll()
    {
        AddAuthHeader();
        return await _http.GetFromJsonAsync<List<Employee>>(
            "https://localhost:5002/api/employees") ?? [];
    }
}

public record Employee(
    Guid EmployeeId,
    string Name,
    string Department,
    decimal Salary
);
