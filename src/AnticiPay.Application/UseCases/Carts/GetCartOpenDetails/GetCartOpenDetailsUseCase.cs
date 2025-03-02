using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Domain.Services.Tax;
using AnticiPay.Domain.Services.TotalSpendByCompany;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpenDetails;
public class GetCartOpenDetailsUseCase : IGetCartOpenDetailsUseCase
{
    private readonly ICartReadOnlyRepository _cartReadOnlyRepository;
    private readonly ITotalSpendByCompany _totalSpendByCompany;
    private readonly ILoggedCompany _loggedCompany;
    private readonly ITaxService _taxService;

    public GetCartOpenDetailsUseCase(
        ICartReadOnlyRepository cartReadOnlyRepository,
        ITotalSpendByCompany totalSpendByCompany,
        ILoggedCompany loggedCompany,
        ITaxService taxService)
    {
        _cartReadOnlyRepository = cartReadOnlyRepository;
        _totalSpendByCompany = totalSpendByCompany;
        _loggedCompany = loggedCompany;
        _taxService = taxService;
    }

    public async Task<ResponseCartDetailsJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var cart = await _cartReadOnlyRepository.GetOpenCartByCompany(loggedCompany.Id);

        var totalSpentThisMonth = await _totalSpendByCompany.Get(loggedCompany.Id);

        return new ResponseCartDetailsJson
        {
            CompanyName = loggedCompany.Name,
            Cnpj = loggedCompany.Cnpj,
            CreditLimit = Math.Round(loggedCompany.GetCreditLimit() - totalSpentThisMonth, 2),
            Invoices = cart?.Invoices.Select(invoice => new ResponseInvoiceDetailsJson
            {
                Id = invoice.Id,
                Number = invoice.Number,
                DueDate = invoice.DueDate,
                GrossValue = Math.Round(invoice.Amount, 2),
                NetValue = invoice.CalculateNetValue(_taxService)
            }).ToList() ?? [],
            TotalNetValue = Math.Round(cart?.CalculateTotalNetValue(_taxService) ?? 0, 2),
            TotalGrossValue = Math.Round(cart?.TotalAmount ?? 0, 2)
        };
    }
}
