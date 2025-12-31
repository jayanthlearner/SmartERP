namespace WorkflowService.Models;

public class WorkflowDefinition
{
    public Guid WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RulesJson { get; set; } = string.Empty;
}
