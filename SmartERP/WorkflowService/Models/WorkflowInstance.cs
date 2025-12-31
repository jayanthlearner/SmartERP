namespace WorkflowService.Models;

public class WorkflowInstance
{
    public Guid InstanceId { get; set; }
    public Guid WorkflowId { get; set; }
    public Guid EntityId { get; set; }
    public string Status { get; set; } = "Pending";
    public string CurrentApproverRole { get; set; } = string.Empty;
}
