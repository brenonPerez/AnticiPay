using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Repositories.Carts;
using Microsoft.EntityFrameworkCore;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Carts;
internal class CartRepository : ICartWriteOnlyRepository, ICartUpdateOnlyRepository
{
    private readonly AnticiPayDbContext _dbContext;

    public CartRepository(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Cart cart)
    {
        await _dbContext.Carts.AddAsync(cart);
    }

    public void AttachCompany(Company company)
    {
        _dbContext.Companies.Attach(company);
        _dbContext.Entry(company).State = EntityState.Unchanged;
    }

    public async Task<Cart?> GetCartOpenByCompany(long companyId)
    {
        return await _dbContext.Carts
            .Include(c => c.Company)
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.Status == CartStatus.Open);
    }

    public async Task<Cart?> GetCartByIdAsync(long cartId)
    {
        return await _dbContext.Carts
            .Include(c => c.Company)
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public void Update(Cart cart)
    {
        _dbContext.Carts.Update(cart);
    }
}
