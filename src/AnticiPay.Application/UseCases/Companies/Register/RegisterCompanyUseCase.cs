using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Exception.ExceptionsBase;
using AutoMapper;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _companyRepository;
    private readonly IMapper _mapper;
    public RegisterCompanyUseCase(ICompanyWriteOnlyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request)
    {
        Validate(request);

        var company = _mapper.Map<Company>(request);

        await _companyRepository.Add(company);

        return _mapper.Map<ResponseRegisteredCompanyJson>(company);
    }

    public static void Validate(RequestRegisterCompanyJson request)
    {
        var result = new RegisterCompanyValidator().Validate(request);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
