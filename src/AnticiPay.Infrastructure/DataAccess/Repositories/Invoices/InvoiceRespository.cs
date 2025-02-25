using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Invoices;
using Microsoft.EntityFrameworkCore;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Invoices;
internal class InvoiceRespository : IInvoiceWriteOnlyRepository, IInvoiceReadOnlyRepository, IInvoiceUpdateOnlyRepository
{
    private readonly AnticiPayDbContext _dbContext;
    public InvoiceRespository(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
    }

    public async Task<bool> ExistInvoiceWithNumber(string number)
    {
        return await _dbContext.Invoices.AnyAsync(i => i.Number.Equals(number));
    }

    public async Task<Invoice?> GetInvoiceNotInCart(long invoiceId)
    {
        return await _dbContext.Invoices
            .Where(i => i.Id == invoiceId && i.CartId == null)
            .FirstOrDefaultAsync();
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoices.Update(invoice);
    }
}
