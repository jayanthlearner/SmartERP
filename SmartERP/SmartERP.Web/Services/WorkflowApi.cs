using System.Net.Http.Headers;
using System.Net.Http.Json;

public class WorkflowApi
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public WorkflowApi(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AddAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);
    }

    public async Task<WorkflowInstance> ExecuteWorkflow(
        Guid workflowId,
        Guid entityId,
        decimal amount)
    {
        AddAuthHeader();

        var response = await _http.PostAsJsonAsync(
            "https://localhost:5004/api/workflows/execute",
            new
            {
                workflowId,
                entityId,
                amount
            });

        return await response.Content.ReadFromJsonAsync<WorkflowInstance>();
    }
}

public record WorkflowInstance(
    Guid InstanceId,
    string Status,
    string CurrentApproverRole
);
