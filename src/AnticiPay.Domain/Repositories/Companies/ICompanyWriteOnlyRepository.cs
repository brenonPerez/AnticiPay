using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Companies;
public interface ICompanyWriteOnlyRepository
{
    Task Add(Company company);
}
