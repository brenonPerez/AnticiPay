namespace AnticiPay.Communication.Responses;
public class ResponseInvoiceDetailsJson
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
}
