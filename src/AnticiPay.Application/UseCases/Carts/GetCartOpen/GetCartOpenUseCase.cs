﻿using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Exception.Exceptions;
using AnticiPay.Exception.Resources;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpen;
public class GetCartOpenUseCase : IGetCartOpenUseCase
{
    private readonly ICartReadOnlyRepository _cartReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;

    public GetCartOpenUseCase(
        ICartReadOnlyRepository cartReadOnlyRepository,
        ILoggedCompany loggedCompany)
    {
        _cartReadOnlyRepository = cartReadOnlyRepository;
        _loggedCompany = loggedCompany;
    }

    public async Task<ResponseCartJson> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var cart = await _cartReadOnlyRepository.GetOpenCartByCompany(loggedCompany.Id);

        return new ResponseCartJson
        {
            Id = cart?.Id ?? 0,
            InvoiceCount = cart?.Invoices.Count ?? 0,
        };
    }
}
