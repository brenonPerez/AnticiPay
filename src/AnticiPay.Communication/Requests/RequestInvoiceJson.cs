namespace AnticiPay.Communication.Requests;
public class RequestInvoiceJson
{
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}
