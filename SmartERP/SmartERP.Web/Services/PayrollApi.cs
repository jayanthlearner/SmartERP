using System.Net.Http.Headers;
using System.Net.Http.Json;

public class PayrollApi
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public PayrollApi(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AddAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);
    }

    public async Task<List<Payroll>> GetMonthly(int month, int year)
    {
        AddAuthHeader();

        return await _http.GetFromJsonAsync<List<Payroll>>(
            $"https://localhost:5003/api/payroll/{month}/{year}") ?? [];
    }
}

public record Payroll(
    Guid PayrollId,
    Guid EmployeeId,
    decimal NetSalary,
    int Month,
    int Year
);
