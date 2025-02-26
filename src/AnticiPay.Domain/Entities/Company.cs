using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Utils;
using System.Data;

namespace AnticiPay.Domain.Entities;
public class Company
{
    public long Id { get; set; }

    private string _cnpj = string.Empty;
    public string Cnpj
    {
        get => _cnpj;
        set => _cnpj = CnpjUtils.Normalize(value);
    }
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public BusinessType BusinessType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid CompanyIdentifier { get; set; }

    public decimal GetCreditLimit()
    {
        if (MonthlyRevenue >= 10_000 && MonthlyRevenue <= 50_000)
            return MonthlyRevenue * 0.5m;
        if (MonthlyRevenue >= 50_001 && MonthlyRevenue <= 100_000)
            return BusinessType == BusinessType.Services ? MonthlyRevenue * 0.55m : MonthlyRevenue * 0.60m;
        if (MonthlyRevenue > 100_000)
            return BusinessType == BusinessType.Services ? MonthlyRevenue * 0.60m : MonthlyRevenue * 0.65m;
        return 0;
    }
}
