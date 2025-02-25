using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Invoices.Register;
public interface IRegisterInvoiceUseCase
{
    Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request);
}
