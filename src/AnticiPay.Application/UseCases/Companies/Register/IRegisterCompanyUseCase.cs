using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Companies.Register;
public interface IRegisterCompanyUseCase
{
    Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request);
}
