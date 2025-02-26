using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Carts;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Carts;
internal class ValidatedCartRepository : ICartUpdateOnlyRepository
{
    private readonly ICartUpdateOnlyRepository _updateOnlyRepository;

    public ValidatedCartRepository(ICartUpdateOnlyRepository updateOnlyRepository)
    {
        _updateOnlyRepository = updateOnlyRepository;
    }

    public void AttachCompany(Company company)
    {
        _updateOnlyRepository.AttachCompany(company);
    }

    public async Task<Cart?> GetCartOpenByCompany(long companyId)
    {
        var cart = await _updateOnlyRepository.GetCartOpenByCompany(companyId);
        RemoveExpiredInvoices(cart);
        return cart;
    }

    public void Update(Cart cart)
    {
        RemoveExpiredInvoices(cart);
        _updateOnlyRepository.Update(cart);
    }

    private static void RemoveExpiredInvoices(Cart? cart)
    {
        if (cart != null)
            cart.Invoices = cart.Invoices
                .Where(invoice => invoice.DueDate.Date > DateTime.UtcNow.Date)
                .ToList();
    }
}
