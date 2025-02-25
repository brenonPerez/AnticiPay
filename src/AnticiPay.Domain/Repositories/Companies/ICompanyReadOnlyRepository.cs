using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Companies;
public interface ICompanyReadOnlyRepository
{
    Task<bool> ExistActiveCompanyWithEmail(string email);

    Task<bool> ExistActiveCompanyWhithCnpj(string cnpj);

    Task<Company?> GetCompanyByEmail(string email);
}
