using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowService.Data;
using WorkflowService.DTOs;
using WorkflowService.Models;
using WorkflowService.Services;

namespace WorkflowService.Controllers;

[ApiController]
[Route("api/workflows")]
[Authorize] // 🔐 JWT required
public class WorkflowController : ControllerBase
{
    private readonly WorkflowDbContext _context;
    private readonly WorkflowEngine _engine;

    public WorkflowController(WorkflowDbContext context, WorkflowEngine engine)
    {
        _context = context;
        _engine = engine;
    }

    // Create workflow
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateWorkflow(CreateWorkflowDto dto)
    {
        var workflow = new WorkflowDefinition
        {
            WorkflowId = Guid.NewGuid(),
            Name = dto.Name,
            RulesJson = dto.RulesJson
        };

        _context.WorkflowDefinitions.Add(workflow);
        await _context.SaveChangesAsync();

        return Ok(workflow);
    }

    // Execute workflow
    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteWorkflow(ExecuteWorkflowDto dto)
    {
        var workflow = await _context.WorkflowDefinitions.FindAsync(dto.WorkflowId);
        if (workflow == null) return NotFound("Workflow not found");

        var approverRole = _engine.Evaluate(workflow.RulesJson, dto.Amount);

        var instance = new WorkflowInstance
        {
            InstanceId = Guid.NewGuid(),
            WorkflowId = workflow.WorkflowId,
            EntityId = dto.EntityId,
            Status = "Pending",
            CurrentApproverRole = approverRole
        };

        _context.WorkflowInstances.Add(instance);
        await _context.SaveChangesAsync();

        return Ok(instance);
    }

    // Get workflow status
    [HttpGet("instance/{id}")]
    public async Task<IActionResult> GetInstance(Guid id)
    {
        var instance = await _context.WorkflowInstances.FindAsync(id);
        return instance == null ? NotFound() : Ok(instance);
    }
}
