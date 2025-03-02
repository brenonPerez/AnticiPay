using AnticiPay.Communication.Requests;
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

    public async Task<ResponseInvoicesJson> Execute(RequestFilterInvoicesJson request)
    {
        var loggedCompany = await _loggedCompany.Get();
        var invoices = await _invoiceReadOnlyRepository.GetAllByCompany(loggedCompany.Id, request.Number, request.Amount, request.DueDate, request.PageIndex, request.PageSize);

        var totalCount = await _invoiceReadOnlyRepository.GetTotalCountByCompany(loggedCompany.Id);

        var response = new ResponseInvoicesJson
        {
            Invoices = _mapper.Map<List<ResponseInvoiceJson>>(invoices),
            Meta = new ResponseInvoicesMetaJson
            {
                PageIndex = request.PageIndex,
                PerPage = request.PageSize,
                TotalCount = totalCount,
            }
        };

        return response;
    }
}
