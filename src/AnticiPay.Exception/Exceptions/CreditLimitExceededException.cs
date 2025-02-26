using AnticiPay.Exception.Exceptions.Base;
using AnticiPay.Exception.Resources;
using System.Net;

namespace AnticiPay.Exception.Exceptions;
public class CreditLimitExceededException : AnticiPayException
{
    public CreditLimitExceededException() : base(ResourceErrorMessages.CREDIT_LIMIT_EXCEEDED)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
