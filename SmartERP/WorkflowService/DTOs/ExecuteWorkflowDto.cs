namespace WorkflowService.DTOs;

public class ExecuteWorkflowDto
{
    public Guid WorkflowId { get; set; }
    public Guid EntityId { get; set; }
    public decimal Amount { get; set; }
}
