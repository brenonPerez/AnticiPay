using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Repositories.Companies;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyRepository _companyRepository;
    public RegisterCompanyUseCase(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request)
    {
        var company = new Company
        {
            Name = request.Name,
            Cnpj = request.Cnpj,
            MonthlyRevenue = request.MonthlyRevenue,
            BusinessType = (BusinessType)request.BusinessType,
            Email = request.Email,
            Password = request.Password
        };
        await _companyRepository.Add(company);
        return new ResponseRegisteredCompanyJson{
            Name = company.Name,
        };
    }
}
