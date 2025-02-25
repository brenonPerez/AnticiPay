using System.Text.RegularExpressions;

namespace AnticiPay.Domain.Utils;
public static class CnpjUtils
{
    public static string Normalize(string cnpj)
    {
        return string.IsNullOrWhiteSpace(cnpj) ? string.Empty : Regex.Replace(cnpj, @"\D", "");
    }
}
