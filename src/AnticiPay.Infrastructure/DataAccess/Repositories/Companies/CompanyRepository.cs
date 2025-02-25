using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Companies;
using Microsoft.EntityFrameworkCore;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Companies;
internal class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository
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

    public async Task<bool> ExistActiveCompanyWhithCnpj(string cnpj)
    {
        return await _dbContext.Companies.AnyAsync(c => c.Cnpj.Equals(cnpj));
    }

    public async Task<bool> ExistActiveCompanyWithEmail(string email)
    {
        return await _dbContext.Companies.AnyAsync(c => c.Email.Equals(email));
    }

    public async Task<Company?> GetCompanyByEmail(string email)
    {
        return await _dbContext.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email.Equals(email));
    }
}
