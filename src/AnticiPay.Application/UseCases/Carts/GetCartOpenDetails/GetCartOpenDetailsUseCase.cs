using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Domain.Services.Tax;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpenDetails;
public class GetCartOpenDetailsUseCase : IGetCartOpenDetailsUseCase
{
    private readonly ICartReadOnlyRepository _cartReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly ITaxService _taxService;

    public GetCartOpenDetailsUseCase(
        ICartReadOnlyRepository cartReadOnlyRepository,
        ILoggedCompany loggedCompany,
        ITaxService taxService)
    {
        _cartReadOnlyRepository = cartReadOnlyRepository;
        _loggedCompany = loggedCompany;
        _taxService = taxService;
    }

    public async Task<ResponseCartDetailsJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var cart = await _cartReadOnlyRepository.GetOpenCartByCompany(loggedCompany.Id);

        if (cart is null)
        {
            throw new NotFoundException(ResourceErrorMessages.CART_IS_EMPTY);
        }

        return new ResponseCartDetailsJson
        {
            CompanyName = loggedCompany.Name,
            Cnpj = loggedCompany.Cnpj,
            CreditLimit = Math.Round(loggedCompany.GetCreditLimit(), 2),
            Invoices = cart.Invoices.Select(invoice => new ResponseInvoiceDetailsJson
            {
                Number = invoice.Number,
                GrossValue = Math.Round(invoice.Amount, 2),
                NetValue = invoice.CalculateNetValue(_taxService)
            }).ToList(),
            TotalNetValue = Math.Round(cart.CalculateTotalNetValue(_taxService), 2),
            TotalGrossValue = Math.Round(cart.TotalAmount, 2)
        };
    }
}
