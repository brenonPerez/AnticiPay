namespace AnticiPay.Communication.Requests;
public class RequestFilterInvoicesJson
{
    public string? Number { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? DueDate { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
