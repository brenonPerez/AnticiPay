using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Invoices;
public interface IInvoiceUpdateOnlyRepository
{
    Task<Invoice?> GetInvoiceNotInCart(long invoiceId);

    void Update(Invoice invoice);

    Task<Invoice?> GetInvoiceFromOpenCart(long invoiceId, long companyId);
}
