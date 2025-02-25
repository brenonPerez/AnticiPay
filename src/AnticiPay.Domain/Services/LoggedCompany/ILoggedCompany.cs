using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Services.LoggedCompany;
public interface ILoggedCompany
{
    Task<Company> Get();
}
