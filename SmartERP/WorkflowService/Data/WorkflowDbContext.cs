using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkflowService.Models;

namespace WorkflowService.Data;

public class WorkflowDbContext : DbContext
{
    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
        : base(options) { }

    public DbSet<WorkflowDefinition> WorkflowDefinitions => Set<WorkflowDefinition>();
    public DbSet<WorkflowInstance> WorkflowInstances => Set<WorkflowInstance>();
}
