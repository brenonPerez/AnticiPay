using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpenDetails;
public class GetCartOpenDetailsUseCase : IGetCartOpenDetailsUseCase
{
    private readonly ICartReadOnlyRepository _cartReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;

    public GetCartOpenDetailsUseCase(
        ICartReadOnlyRepository cartReadOnlyRepository,
        ILoggedCompany loggedCompany)
    {
        _cartReadOnlyRepository = cartReadOnlyRepository;
        _loggedCompany = loggedCompany;
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
            CreditLimit = loggedCompany.GetCreditLimit(),
            Invoices = cart.Invoices.Select(invoice => new ResponseInvoiceDetailsJson
            {
                Number = invoice.Id,
                GrossValue = invoice.Amount,
                NetValue = invoice.Amount * 0.953488m
            }).ToList(),
            TotalNetValue = cart.Invoices.Sum(invoice => invoice.Amount * 0.953488m),
            TotalGrossValue = cart.Invoices.Sum(invoice => invoice.Amount)
        };
    }
}
