namespace WorkflowService.DTOs;

public class CreateWorkflowDto
{
    public string Name { get; set; } = string.Empty;
    public string RulesJson { get; set; } = string.Empty;
}
