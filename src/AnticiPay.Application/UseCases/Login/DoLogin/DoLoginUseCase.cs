using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Domain.Security.Cryptography;
using AnticiPay.Domain.Security.Tokens;
using AnticiPay.Exception.Exceptions;

namespace AnticiPay.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly ICompanyReadOnlyRepository companyReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(ICompanyReadOnlyRepository companyReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        this.companyReadOnlyRepository = companyReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestLoginJson request)
    {
        var company = await companyReadOnlyRepository.GetCompanyByEmail(request.Email);

        if (company == null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncripter.Compare(request.Password, company.Password);

        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredCompanyJson
        {
            Name = company.Name,
            Token = _accessTokenGenerator.Generate(company)
        };
    }
}
