using AnticiPay.Domain.Services.Tax;
using Microsoft.Extensions.Configuration;

namespace AnticiPay.Infrastructure.Services.Tax;
internal class TaxService : ITaxService
{
    public decimal MonthlyTaxRate { get; }

    public TaxService(IConfiguration configuration)
    {
        MonthlyTaxRate = configuration.GetValue<decimal>("Settings:Tax:MonthlyRate");
    }

    public decimal CalculateNetValue(decimal grossValue, DateTime dueDate)
    {
        var daysUntilDue = (dueDate.Date - DateTime.UtcNow.Date).Days;

        var netValue = grossValue / (decimal)Math.Pow(1 + (double)MonthlyTaxRate, (double)daysUntilDue / 30);

        return Math.Round(netValue, 2);
    }
}
