namespace PayrollService.Services;

public class PayrollCalculator
{
    public decimal CalculateTax(decimal salary)
    {
        // Simple slab logic (example)
        if (salary <= 25000) return salary * 0.05m;
        if (salary <= 50000) return salary * 0.10m;
        return salary * 0.15m;
    }

    public decimal CalculatePF(decimal salary)
    {
        return salary * 0.12m; // 12% PF
    }

    public decimal CalculateNet(decimal salary)
    {
        var tax = CalculateTax(salary);
        var pf = CalculatePF(salary);
        return salary - tax - pf;
    }
}
