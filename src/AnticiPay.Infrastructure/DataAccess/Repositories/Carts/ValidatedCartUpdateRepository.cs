using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Extensions;
using AnticiPay.Domain.Repositories.Carts;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Carts;
internal class ValidatedCartUpdateRepository : ICartUpdateOnlyRepository
{
    private readonly ICartUpdateOnlyRepository _updateOnlyRepository;

    public ValidatedCartUpdateRepository(ICartUpdateOnlyRepository updateOnlyRepository)
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
        cart.RemoveExpiredInvoices();
        return cart;
    }

    public void Update(Cart cart)
    {
        cart.RemoveExpiredInvoices();
        _updateOnlyRepository.Update(cart);
    }
}
