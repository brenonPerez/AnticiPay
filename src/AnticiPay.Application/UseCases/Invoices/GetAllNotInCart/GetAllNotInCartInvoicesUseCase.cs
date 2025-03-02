using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Domain.Services.Tax;
using AutoMapper;

namespace AnticiPay.Application.UseCases.Invoices.GetAllNotInCart;
public class GetAllNotInCartInvoicesUseCase : IGetAllNotInCartInvoicesUseCase
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly ITaxService _taxService;
    private readonly IMapper _mapper;

    public GetAllNotInCartInvoicesUseCase(IInvoiceReadOnlyRepository invoiceReadOnlyRepository, 
        ILoggedCompany loggedCompany,
        ITaxService taxService,
        IMapper mapper)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
        _loggedCompany = loggedCompany;
        _taxService = taxService;
        _mapper = mapper;
    }

    public async Task<ResponseInvoicesSimulatedJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var invoices = await _invoiceReadOnlyRepository.GetAllNotInCartByCompany(loggedCompany.Id);

        var response = new ResponseInvoicesSimulatedJson
        {
            Invoices = invoices.Select(invoice =>
            {
                var responseInvoice = _mapper.Map<ResponseInvoiceSimulatedJson>(invoice);
                responseInvoice.AmountReceivable = invoice.CalculateNetValue(_taxService);
                return responseInvoice;
            }).ToList()
        };

        return response;
    }
}
