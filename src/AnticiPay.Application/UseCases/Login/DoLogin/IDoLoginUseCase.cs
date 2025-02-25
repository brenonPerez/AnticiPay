using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    Task<ResponseRegisteredCompanyJson> Execute(RequestLoginJson request);
}
