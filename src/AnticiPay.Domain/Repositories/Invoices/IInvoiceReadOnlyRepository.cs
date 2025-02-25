namespace AnticiPay.Domain.Repositories.Invoices;
public interface IInvoiceReadOnlyRepository
{
    Task<bool> ExistInvoiceWithNumber(string number);
}
