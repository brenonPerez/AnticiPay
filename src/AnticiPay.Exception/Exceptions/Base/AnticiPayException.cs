namespace AnticiPay.Exception.Exceptions.Base;
public abstract class AnticiPayException : SystemException
{
    public AnticiPayException(string message) : base(message)
    {
    }
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
