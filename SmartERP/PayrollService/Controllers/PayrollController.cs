using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollService.Data;
using PayrollService.DTOs;
using PayrollService.Models;
using PayrollService.Services;

namespace PayrollService.Controllers;

[ApiController]
[Route("api/payroll")]
[Authorize] // 🔐 JWT required
public class PayrollController : ControllerBase
{
    private readonly PayrollDbContext _context;
    private readonly PayrollCalculator _calculator;

    public PayrollController(PayrollDbContext context, PayrollCalculator calculator)
    {
        _context = context;
        _calculator = calculator;
    }

    // POST: api/payroll/generate
    [HttpPost("generate")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GeneratePayroll(GeneratePayrollDto dto)
    {
        var tax = _calculator.CalculateTax(dto.BasicSalary);
        var pf = _calculator.CalculatePF(dto.BasicSalary);
        var net = _calculator.CalculateNet(dto.BasicSalary);

        var payroll = new Payroll
        {
            PayrollId = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            Month = dto.Month,
            Year = dto.Year,
            BasicSalary = dto.BasicSalary,
            Tax = tax,
            PF = pf,
            NetSalary = net
        };

        _context.Payrolls.Add(payroll);
        await _context.SaveChangesAsync();

        return Ok(payroll);
    }

    // GET: api/payroll/employee/{employeeId}
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(Guid employeeId)
    {
        var payrolls = await _context.Payrolls
            .Where(p => p.EmployeeId == employeeId)
            .ToListAsync();

        return Ok(payrolls);
    }

    // GET: api/payroll/{month}/{year}
    [HttpGet("{month}/{year}")]
    public async Task<IActionResult> GetByMonth(int month, int year)
    {
        return Ok(await _context.Payrolls
            .Where(p => p.Month == month && p.Year == year)
            .ToListAsync());
    }
}
