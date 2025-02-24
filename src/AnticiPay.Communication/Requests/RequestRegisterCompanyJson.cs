using AnticiPay.Communication.Enums;

namespace AnticiPay.Communication.Requests;
public class RequestRegisterCompanyJson
{
    public string Cnpj { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; } = 0;
    public BusinessType BusinessType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
