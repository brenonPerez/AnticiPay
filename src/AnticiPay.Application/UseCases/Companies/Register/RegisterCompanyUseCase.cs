using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Domain.Utils;
using AnticiPay.Exception.ExceptionsBase;
using AutoMapper;
using FluentValidation.Results;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _companyWriteOnlyRepository;
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;
    private readonly IMapper _mapper;
    public RegisterCompanyUseCase(ICompanyWriteOnlyRepository companyWriteOnlyRepository,
        ICompanyReadOnlyRepository companyReadOnlyRepository,
        IMapper mapper)
    {
        _companyWriteOnlyRepository = companyWriteOnlyRepository;
        _companyReadOnlyRepository = companyReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request)
    {
        await Validate(request);

        var company = _mapper.Map<Company>(request);

        await _companyWriteOnlyRepository.Add(company);

        return _mapper.Map<ResponseRegisteredCompanyJson>(company);
    }

    public async Task Validate(RequestRegisterCompanyJson request)
    {
        var result = new RegisterCompanyValidator().Validate(request);

        var emailAlreadyExists = await _companyReadOnlyRepository.ExistActiveCompanyWithEmail(request.Email);
        if (emailAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Email already exists"));
        }

        var cnpjAlreadyExists = await _companyReadOnlyRepository.ExistActiveCompanyWhithCnpj(CnpjUtils.Normalize(request.Cnpj));
        if (cnpjAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Cnpj already exists"));
        }
        
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
