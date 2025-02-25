namespace AnticiPay.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
