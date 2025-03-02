namespace AnticiPay.Domain.Services.TotalSpendByCompany;
public interface ITotalSpendByCompany
{
    Task<decimal> Get(long companyId);
}
