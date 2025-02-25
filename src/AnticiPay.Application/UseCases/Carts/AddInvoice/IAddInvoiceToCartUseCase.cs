using AnticiPay.Communication.Requests;

namespace AnticiPay.Application.UseCases.Carts.AddInvoice;
public interface IAddInvoiceToCartUseCase
{
    Task Execute(RequestInvoiceCartJson request);
}
