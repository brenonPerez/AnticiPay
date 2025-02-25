using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Carts;
public interface ICartUpdateOnlyRepository
{
    void Update(Cart cart);
    Task<Cart?> GetCartOpenByCompany(long companyId);
}
