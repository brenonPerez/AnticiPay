using AnticiPay.Exception.Exceptions.Base;
using AnticiPay.Exception.Resources;
using System.Net;

namespace AnticiPay.Exception.Exceptions;
public class InvoiceExpiredException : AnticiPayException
{
    public InvoiceExpiredException() : base(ResourceErrorMessages.INVOICE_EXPIRED)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
