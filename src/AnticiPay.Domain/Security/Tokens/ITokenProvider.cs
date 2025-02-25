namespace AnticiPay.Domain.Security.Tokens;
public interface ITokenProvider
{
    string TokenOnRequest();
}
