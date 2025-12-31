namespace PayrollService.DTOs;

public class GeneratePayrollDto
{
    public Guid EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
