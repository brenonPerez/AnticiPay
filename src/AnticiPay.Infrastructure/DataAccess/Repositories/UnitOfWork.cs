using AnticiPay.Domain.Repositories;

namespace AnticiPay.Infrastructure.DataAccess.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly AnticiPayDbContext _dbContext;
    public UnitOfWork(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
