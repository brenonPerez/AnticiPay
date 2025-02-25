using AnticiPay.Exception.Exceptions.Base;
using System.Net;

namespace AnticiPay.Exception.Exceptions;
public class InvalidLoginException : AnticiPayException
{
    public InvalidLoginException() : base("Email or password invalid")
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
