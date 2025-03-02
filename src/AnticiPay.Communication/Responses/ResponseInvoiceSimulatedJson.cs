namespace AnticiPay.Communication.Responses;
public class ResponseInvoiceSimulatedJson
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public decimal AmountReceivable {  get; set; }
}
