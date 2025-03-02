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
        return await _dbContext.Invoices
            .AsNoTracking()
            .AnyAsync(i => i.Number.Equals(number));
    }

    public async Task<List<Invoice>> GetAllByCompany(long companyId, string? number, decimal? amount, DateTime? dueDate, int pageIndex, int pageSize)
    {
        var query = _dbContext.Invoices.AsNoTracking().AsQueryable();

        query = query.Where(i => i.CompanyId == companyId);

        if (!string.IsNullOrEmpty(number))
        {
            query = query.Where(i => i.Number.Contains(number));
        }

        if (amount.HasValue && amount.Value != 0)
        {
            query = query.Where(i => i.Amount == amount);
        }

        if (dueDate.HasValue)
        {
            query = query.Where(i => i.DueDate == dueDate);
        }

        query = query.OrderByDescending(i => i.Id)
                     .Skip((pageIndex) * pageSize)
                     .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<List<Invoice>> GetAllNotInCartByCompany(long companyId)
    {
        return await _dbContext.Invoices
            .AsNoTracking()
            .Where(i => i.CompanyId == companyId && i.CartId == null && i.DueDate.Date > DateTime.UtcNow.Date)
            .OrderByDescending(i => i.Id)
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
    public async Task<long> GetTotalCountByCompany(long companyId)
    {
        return await _dbContext.Invoices
            .AsNoTracking()
            .Where(i => i.CompanyId == companyId)
            .CountAsync();
    }
}
