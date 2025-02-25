using AnticiPay.Communication.Requests;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Repositories;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Repositories.Invoices;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.AddInvoice;
public class AddInvoiceToCartUseCase : IAddInvoiceToCartUseCase
{
    private readonly IInvoiceUpdateOnlyRepository _invoiceUpdateOnlyRepository;
    private readonly ICartUpdateOnlyRepository _cartUpdateOnlyRepository;
    private readonly ICartWriteOnlyRepository _cartWriteOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly IUnitOfWork _unitOfWork;

    public AddInvoiceToCartUseCase(IInvoiceUpdateOnlyRepository invoiceReadOnlyRepository,
        ICartUpdateOnlyRepository cartUpdateOnlyRepository,
        ICartWriteOnlyRepository cartWriteOnlyRepository,
        ILoggedCompany loggedCompany,
        IUnitOfWork unitOfWork)
    {
        _invoiceUpdateOnlyRepository = invoiceReadOnlyRepository;
        _cartUpdateOnlyRepository = cartUpdateOnlyRepository;
        _cartWriteOnlyRepository = cartWriteOnlyRepository;
        _loggedCompany = loggedCompany;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestInvoiceCartJson request)
    {
        var invoice = await GetInvoice(request.InvoiceId);
        var loggedCompany = await _loggedCompany.Get();
        await UpdateOrCreateCart(loggedCompany.Id, invoice);

        await _unitOfWork.Commit();
    }

    private async Task<Invoice> GetInvoice(long invoiceId)
    {
        var invoice = await _invoiceUpdateOnlyRepository.GetInvoiceNotInCart(invoiceId);
        if (invoice is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }
        return invoice;
    }

    private async Task<Cart> UpdateOrCreateCart(long companyId, Invoice invoice)
    {
        var cart = await _cartUpdateOnlyRepository.GetCartOpenByCompany(companyId);
        if (cart is null)
        {
            cart = new Cart
            {
                CompanyId = companyId,
                Invoices = new List<Invoice> { invoice }
            };
            await _cartWriteOnlyRepository.Add(cart);
        }
        else
        {
            cart.Invoices.Add(invoice);
            _invoiceUpdateOnlyRepository.Update(invoice);
        }
        return cart;
    }
}
