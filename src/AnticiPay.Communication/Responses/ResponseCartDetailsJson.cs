namespace AnticiPay.Communication.Responses;
public class ResponseCartDetailsJson
{
    public string CompanyName { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public decimal CreditLimit { get; set; }
    public List<ResponseInvoiceDetailsJson> Invoices { get; set; } = new();
    public decimal TotalNetValue { get; set; }
    public decimal TotalGrossValue { get; set; }
}
