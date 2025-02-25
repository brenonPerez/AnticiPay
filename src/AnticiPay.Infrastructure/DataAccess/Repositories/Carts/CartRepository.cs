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

    public Task<Cart?> GetCartOpenByCompany(long companyId)
    {
        return _dbContext.Carts
            .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.Status == CartStatus.Open);
    }

    public void Update(Cart cart)
    {
        _dbContext.Carts.Update(cart);
    }
}
