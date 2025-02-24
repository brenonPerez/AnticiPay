using AnticiPay.Domain.Enums;

namespace AnticiPay.Domain.Entities;
public class Company
{
    public long Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public BusinessType BusinessType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid CompanyIdentifier { get; set; }
}
