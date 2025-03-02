using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Services.TotalSpendByCompany;
using AnticiPay.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AnticiPay.Infrastructure.Services.TotalSpendByCompany;
internal class TotalSpendByCompany : ITotalSpendByCompany
{
    private readonly AnticiPayDbContext _dbContext;

    public TotalSpendByCompany(AnticiPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<decimal> Get(long companyId)
    {
        var currentMonth = DateTime.UtcNow.Month;
        var currentYear = DateTime.UtcNow.Year;

        var totalSpend = await _dbContext.Invoices
            .AsNoTracking()
            .Where(invoice => invoice.CompanyId == companyId &&
                              invoice.Cart != null &&
                              invoice.Cart.Status == CartStatus.Closed &&
                              invoice.Cart.CheckoutDate.HasValue &&
                              invoice.Cart.CheckoutDate.Value.Month == currentMonth &&
                              invoice.Cart.CheckoutDate.Value.Year == currentYear)
            .SumAsync(invoice => invoice.Amount);

        return totalSpend;
    }
}
