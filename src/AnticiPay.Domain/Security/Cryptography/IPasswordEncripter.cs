namespace AnticiPay.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    bool Compare(string password, string passwordHash);
    string Encrypt(string password);
}
