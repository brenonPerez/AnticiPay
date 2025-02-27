using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Invoices.GetAll;
public interface IGetAllInvoicesUseCase
{
    Task<ResponseInvoicesJson> Execute();
}
