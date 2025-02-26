using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Extensions;
public static class CartExtensions
{
    public static void RemoveExpiredInvoices(this Cart? cart)
    {
        if (cart != null)
            cart.Invoices = cart.Invoices
                .Where(invoice => invoice.DueDate.Date > DateTime.UtcNow.Date)
                .ToList();
    }
}
