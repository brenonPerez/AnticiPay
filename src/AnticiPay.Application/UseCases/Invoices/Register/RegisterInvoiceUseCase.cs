using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Exception.ExceptionsBase;
using AutoMapper;
using FluentValidation.Results;

namespace AnticiPay.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _invoiceWriteOnlyRepository;
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;
    private readonly IMapper _mapper;
    public RegisterInvoiceUseCase(
        IInvoiceWriteOnlyRepository invoiceWriteOnlyRepository,
        IInvoiceReadOnlyRepository invoiceReadOnlyRepository,
        IMapper mapper)
    {
        _invoiceWriteOnlyRepository = invoiceWriteOnlyRepository;
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
        _mapper = mapper;
    }
    public async Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request)
    {
        await Validate(request);

        var invoice = _mapper.Map<Invoice>(request);
        invoice.CompanyId = 6;

        await _invoiceWriteOnlyRepository.Add(invoice);

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
