using System.Text.RegularExpressions;

namespace AnticiPay.Domain.Extensions;
public static class CnpjExtensions
{
    public static string Normalize(string cnpj)
    {
        return string.IsNullOrWhiteSpace(cnpj) ? string.Empty : Regex.Replace(cnpj, @"\D", "");
    }
}
