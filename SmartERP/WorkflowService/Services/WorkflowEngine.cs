using System.Text.Json;

namespace WorkflowService.Services;

public class WorkflowEngine
{
    public string Evaluate(string rulesJson, decimal amount)
    {
        /*
         Sample Rules JSON:
         {
           "conditions": [
             { "min": 0, "max": 10000, "role": "Manager" },
             { "min": 10001, "max": 50000, "role": "HR" },
             { "min": 50001, "max": 999999, "role": "Finance" }
           ]
         }
        */

        var doc = JsonDocument.Parse(rulesJson);
        var conditions = doc.RootElement.GetProperty("conditions");

        foreach (var condition in conditions.EnumerateArray())
        {
            var min = condition.GetProperty("min").GetDecimal();
            var max = condition.GetProperty("max").GetDecimal();
            var role = condition.GetProperty("role").GetString();

            if (amount >= min && amount <= max)
                return role!;
        }

        return "Admin"; // fallback
    }
}
