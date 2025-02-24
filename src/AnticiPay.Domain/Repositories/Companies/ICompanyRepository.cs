using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Companies;
public interface ICompanyRepository
{
    Task Add(Company company);
}
