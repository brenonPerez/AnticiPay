using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(Company company);
}
