namespace PayrollService.Models;

public class Payroll
{
    public Guid PayrollId { get; set; }
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public decimal BasicSalary { get; set; }
    public decimal Tax { get; set; }
    public decimal PF { get; set; }
    public decimal NetSalary { get; set; }
}
