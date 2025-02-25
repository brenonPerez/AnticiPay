namespace AnticiPay.Communication.Responses;
public class ResponseInvoiceJson
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}
