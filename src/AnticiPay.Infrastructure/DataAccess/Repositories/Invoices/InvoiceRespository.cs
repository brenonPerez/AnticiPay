using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Enums;
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

    public async Task<List<Invoice>> GetAllByCompany(long companyId)
    {
        return await _dbContext.Invoices
            .Where(i => i.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetAllNotInCartByCompany(long companyId)
    {
        return await _dbContext.Invoices
            .Where(i => i.CompanyId == companyId && i.CartId == null)
            .ToListAsync();
    }

    public async Task<Invoice?> GetInvoiceFromOpenCart(long invoiceId, long companyId)
    {
        return await _dbContext.Invoices
            .Include(i => i.Cart)
            .ThenInclude(c => c!.Invoices)
            .Where(i => i.Id == invoiceId && i.CompanyId == companyId && i.CartId != null && i.Cart != null && i.Cart.Status == CartStatus.Open)
            .FirstOrDefaultAsync();
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
