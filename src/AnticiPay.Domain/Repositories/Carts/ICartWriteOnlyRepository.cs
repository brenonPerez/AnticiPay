using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Carts;
public interface ICartWriteOnlyRepository
{
    Task Add(Cart cart);
}
