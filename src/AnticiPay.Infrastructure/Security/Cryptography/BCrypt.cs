using AnticiPay.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace AnticiPay.Infrastructure.Security.Cryptography;
internal class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
