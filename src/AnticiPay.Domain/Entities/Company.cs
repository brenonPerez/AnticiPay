using AnticiPay.Domain.Enums;
using System.Text.RegularExpressions;

namespace AnticiPay.Domain.Entities;
public class Company
{
    public long Id { get; set; }

    private string _cnpj = string.Empty;
    public string Cnpj
    {
        get => _cnpj;
        set => _cnpj = NormalizeCnpj(value);
    }
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public BusinessType BusinessType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid CompanyIdentifier { get; set; }

    private static string NormalizeCnpj(string cnpj)
    {
        return string.IsNullOrWhiteSpace(cnpj)
            ? string.Empty
            : Regex.Replace(cnpj, @"\D", "");
    }
}
