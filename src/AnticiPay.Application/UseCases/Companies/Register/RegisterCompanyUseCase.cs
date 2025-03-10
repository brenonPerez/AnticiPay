﻿using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Extensions;
using AnticiPay.Domain.Repositories;
using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Domain.Security.Cryptography;
using AnticiPay.Domain.Security.Tokens;
using AnticiPay.Exception.ExceptionsBase;
using AnticiPay.Exception.Resources;
using AutoMapper;
using FluentValidation.Results;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _companyWriteOnlyRepository;
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterCompanyUseCase(ICompanyWriteOnlyRepository companyWriteOnlyRepository,
        ICompanyReadOnlyRepository companyReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _companyWriteOnlyRepository = companyWriteOnlyRepository;
        _companyReadOnlyRepository = companyReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request)
    {
        await Validate(request);

        var company = _mapper.Map<Company>(request);
        company.CompanyIdentifier = Guid.NewGuid();
        company.Password = _passwordEncripter.Encrypt(request.Password);

        await _companyWriteOnlyRepository.Add(company);

        await _unitOfWork.Commit();

        return new ResponseRegisteredCompanyJson { 
            Name = company.Name, 
            Token = _accessTokenGenerator.Generate(company) 
        };
    }

    public async Task Validate(RequestRegisterCompanyJson request)
    {
        var result = new RegisterCompanyValidator().Validate(request);

        var emailAlreadyExists = await _companyReadOnlyRepository.ExistActiveCompanyWithEmail(request.Email);
        if (emailAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.CNPJ_ALREADY_EXISTS));
        }

        var cnpjAlreadyExists = await _companyReadOnlyRepository.ExistActiveCompanyWhithCnpj(CnpjExtensions.Normalize(request.Cnpj));
        if (cnpjAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.CNPJ_ALREADY_EXISTS));
        }
        
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
