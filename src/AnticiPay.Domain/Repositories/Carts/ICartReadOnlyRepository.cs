using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Carts;
public interface ICartReadOnlyRepository
{
    Task<Cart?> GetOpenCartByCompany(long companyId);
    Task<decimal?> GetAnticipatedMonthlyTotal(long companyId, int month, int year);
}
