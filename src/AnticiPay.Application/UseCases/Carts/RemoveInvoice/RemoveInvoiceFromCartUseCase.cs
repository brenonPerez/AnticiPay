using AnticiPay.Communication.Requests;
using AnticiPay.Domain.Repositories;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.RemoveInvoice;
public class RemoveInvoiceFromCartUseCase : IRemoveInvoiceFromCartUseCase
{
    private readonly IInvoiceUpdateOnlyRepository _invoiceUpdateOnlyRepository;
    private readonly ICartUpdateOnlyRepository _cartUpdateOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveInvoiceFromCartUseCase(IInvoiceUpdateOnlyRepository invoiceUpdateOnlyRepository,
        ICartUpdateOnlyRepository cartUpdateOnlyRepository,
        ILoggedCompany loggedCompany,
        IUnitOfWork unitOfWork)
    {
        _invoiceUpdateOnlyRepository = invoiceUpdateOnlyRepository;
        _cartUpdateOnlyRepository = cartUpdateOnlyRepository;
        _loggedCompany = loggedCompany;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestInvoiceCartJson request)
    {
        var loggedCompany = await _loggedCompany.Get();
        var invoice = await _invoiceUpdateOnlyRepository.GetInvoiceFromOpenCart(request.InvoiceId, loggedCompany.Id);
        if (invoice is null || invoice.Cart is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        var cart = invoice.Cart;
        cart.Invoices.Remove(invoice);
        _cartUpdateOnlyRepository.Update(cart);

        await _unitOfWork.Commit();
    }
}
