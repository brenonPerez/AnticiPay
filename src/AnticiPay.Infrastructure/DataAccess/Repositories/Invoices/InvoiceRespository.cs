using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Invoices;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Invoices;
internal class InvoiceRespository : IInvoiceWriteOnlyRepository
{
    private readonly AnticiPayDbContext _dbContext;
    public InvoiceRespository(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();
    }
}
