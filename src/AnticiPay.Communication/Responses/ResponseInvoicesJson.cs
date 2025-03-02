namespace AnticiPay.Communication.Responses;
public class ResponseInvoicesJson
{
    public List<ResponseInvoiceJson> Invoices { get; set; } = [];
    public ResponseInvoicesMetaJson Meta { get; set; } = new ResponseInvoicesMetaJson();
}
