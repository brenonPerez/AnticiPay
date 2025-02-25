using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Invoices;
public interface IInvoiceWriteOnlyRepository
{
    Task Add(Invoice invoice);
}
