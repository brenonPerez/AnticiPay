using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Domain.Services.LoggedCompany;
using AutoMapper;

namespace AnticiPay.Application.UseCases.Invoices.GetAll;
public class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly IMapper _mapper;

    public GetAllInvoicesUseCase(
        IInvoiceReadOnlyRepository invoiceReadOnlyRepository,
        ILoggedCompany loggedCompany,
        IMapper mapper)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
        _loggedCompany = loggedCompany;
        _mapper = mapper;
    }

    public async Task<ResponseInvoicesJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var invoices = await _invoiceReadOnlyRepository.GetAllByCompany(loggedCompany.Id);

        var response = new ResponseInvoicesJson
        {
            Invoices = _mapper.Map<List<ResponseInvoiceJson>>(invoices)
        };

        return response;
    }
}
