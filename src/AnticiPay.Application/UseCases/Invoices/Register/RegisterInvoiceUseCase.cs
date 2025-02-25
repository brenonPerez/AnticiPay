using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Exception.ExceptionsBase;
using AutoMapper;
using FluentValidation.Results;

namespace AnticiPay.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _invoiceWriteOnlyRepository;
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterInvoiceUseCase(
        IInvoiceWriteOnlyRepository invoiceWriteOnlyRepository,
        IInvoiceReadOnlyRepository invoiceReadOnlyRepository,
        ILoggedCompany loggedCompany,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _invoiceWriteOnlyRepository = invoiceWriteOnlyRepository;
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
        _loggedCompany = loggedCompany;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request)
    {
        await Validate(request);

        var loggedCompany = await _loggedCompany.Get();

        var invoice = _mapper.Map<Invoice>(request);
        invoice.CompanyId = loggedCompany.Id;

        await _invoiceWriteOnlyRepository.Add(invoice);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }

    private async Task Validate(RequestInvoiceJson request)
    {
        var result = new RegisterInvoiceValidator().Validate(request);

        var numberAlreadyExists = await _invoiceReadOnlyRepository.ExistInvoiceWithNumber(request.Number);
        if (numberAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Number already exists"));
        }

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
