using AnticiPay.Communication.Requests;

namespace AnticiPay.Application.UseCases.Carts.RemoveInvoice;
public interface IRemoveInvoiceFromCartUseCase
{
    Task Execute(RequestInvoiceCartJson request);
}
