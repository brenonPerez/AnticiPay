using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Companies;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Companies;
internal class CompanyRepository : ICompanyWriteOnlyRepository
{
    private readonly AnticiPayDbContext _dbContext;
    public CompanyRepository(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Company company)
    {
        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();
    }
}
