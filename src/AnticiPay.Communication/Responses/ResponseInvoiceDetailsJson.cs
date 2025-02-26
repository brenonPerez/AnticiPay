namespace AnticiPay.Communication.Responses;
public class ResponseInvoiceDetailsJson
{
    public string Number { get; set; } = string.Empty;
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
}
