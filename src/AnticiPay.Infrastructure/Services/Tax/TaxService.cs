using AnticiPay.Domain.Services.Tax;
using Microsoft.Extensions.Configuration;

namespace AnticiPay.Infrastructure.Services.Tax;
internal class TaxService : ITaxService
{
    private readonly decimal _monthlyTaxRate;

    public TaxService(IConfiguration configuration)
    {
        _monthlyTaxRate = configuration.GetValue<decimal>("Settings:Tax:MonthlyRate");
    }

    public decimal CalculateNetValue(decimal grossValue, DateTime dueDate)
    {
        var daysUntilDue = (dueDate.Date - DateTime.UtcNow.Date).Days;

        var netValue = grossValue / (decimal)Math.Pow(1 + (double)_monthlyTaxRate, (double)daysUntilDue / 30);

        return Math.Round(netValue, 2);
    }
}
