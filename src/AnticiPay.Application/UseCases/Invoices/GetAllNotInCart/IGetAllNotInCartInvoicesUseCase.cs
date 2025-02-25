using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Invoices.GetAllNotInCart;
public interface IGetAllNotInCartInvoicesUseCase
{
    Task<ResponseInvoicesJson> Execute();
}
