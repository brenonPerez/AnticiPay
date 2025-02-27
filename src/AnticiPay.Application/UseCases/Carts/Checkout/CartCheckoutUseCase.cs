﻿using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Repositories;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Domain.Services.Tax;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.Checkout;
public class CartCheckoutUseCase : ICartCheckoutUseCase
{
    private readonly ICartUpdateOnlyRepository _cartUpdateOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;
    private readonly ITaxService _taxService;
    private readonly IUnitOfWork _unitOfWork;

    public CartCheckoutUseCase(
        ICartUpdateOnlyRepository cartUpdateOnlyRepository,
        ILoggedCompany loggedCompany,
        ITaxService taxService,
        IUnitOfWork unitOfWork)
    {
        _cartUpdateOnlyRepository = cartUpdateOnlyRepository;
        _loggedCompany = loggedCompany;
        _taxService = taxService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseCartDetailsJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var cart = await _cartUpdateOnlyRepository.GetCartOpenByCompany(loggedCompany.Id);

        if (cart is null || cart.Invoices.Count == 0)
        {
            throw new NotFoundException(ResourceErrorMessages.CART_IS_EMPTY);
        }

        cart.CheckoutDate = DateTime.UtcNow;
        cart.TaxRateAtCheckout = _taxService.MonthlyTaxRate;
        cart.Status = CartStatus.Closed;
        foreach (var invoice in cart.Invoices)
        {
            invoice.NetValueAtCheckout = invoice.CalculateNetValue(_taxService);
        }

        _cartUpdateOnlyRepository.Update(cart);
        await _unitOfWork.Commit();

        return new ResponseCartDetailsJson
        {
            CompanyName = loggedCompany.Name,
            Cnpj = loggedCompany.Cnpj,
            CreditLimit = Math.Round(loggedCompany.GetCreditLimit(), 2),
            Invoices = cart.Invoices.Select(i => new ResponseInvoiceDetailsJson
            {
                Number = i.Number,
                GrossValue = Math.Round(i.Amount, 2),
                NetValue = i.NetValueAtCheckout ?? 0
            }).ToList(),
            TotalNetValue = cart.Invoices.Sum(i => i.NetValueAtCheckout ?? 0),
            TotalGrossValue = cart.Invoices.Sum(i => i.Amount)
        };
    }
}
