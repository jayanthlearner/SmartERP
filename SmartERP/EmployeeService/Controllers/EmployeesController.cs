using EmployeeService.Data;
using EmployeeService.DTOs;
using EmployeeService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize] // 🔐 JWT Required
public class EmployeesController : ControllerBase
{
    private readonly EmployeeDbContext _context;

    public EmployeesController(EmployeeDbContext context)
    {
        _context = context;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Employees.ToListAsync());
    }

    // GET: api/employees/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee == null ? NotFound() : Ok(employee);
    }

    // POST: api/employees
    [HttpPost]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            EmployeeId = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Department = dto.Department,
            Salary = dto.Salary
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    // PUT: api/employees/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeDto dto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        employee.Name = dto.Name;
        employee.Department = dto.Department;
        employee.Salary = dto.Salary;

        await _context.SaveChangesAsync();
        return Ok(employee);
    }

    // DELETE: api/employees/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
