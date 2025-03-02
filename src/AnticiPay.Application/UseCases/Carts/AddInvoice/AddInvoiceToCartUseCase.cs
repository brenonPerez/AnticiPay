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
        var cart = await UpdateOrCreateCart(loggedCompany, invoice);

        if (cart.ExceedsCreditLimit)
        {
            throw new CreditLimitExceededException();
        }

        await _unitOfWork.Commit();
    }

    private async Task<Invoice> GetInvoice(long invoiceId)
    {
        var invoice = await _invoiceUpdateOnlyRepository.GetInvoiceNotInCart(invoiceId);
        if (invoice is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }
        if (invoice.IsExpired)
        {
            throw new InvoiceExpiredException();
        }
        return invoice;
    }

    private async Task<Cart> UpdateOrCreateCart(Company company, Invoice invoice)
    {
        var cart = await _cartUpdateOnlyRepository.GetCartOpenByCompany(company.Id);
        if (cart is null)
        {
            _cartUpdateOnlyRepository.AttachCompany(company);

            cart = new Cart
            {
                CompanyId = company.Id,
                Company = company,
            };
            await _cartWriteOnlyRepository.Add(cart);
            await _unitOfWork.Commit();
        }

        cart.Invoices.Add(invoice);
        _cartUpdateOnlyRepository.Update(cart);

        return cart;
    }
}
