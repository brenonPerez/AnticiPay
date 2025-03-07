﻿using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Extensions;
using AnticiPay.Domain.Repositories.Carts;

namespace AnticiPay.Infrastructure.DataAccess.Repositories.Carts;
internal class ValidatedCartReadRepository : ICartReadOnlyRepository
{
    private readonly ICartReadOnlyRepository _readOnlyRepository;

    public ValidatedCartReadRepository(ICartReadOnlyRepository readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<decimal?> GetAnticipatedMonthlyTotal(long companyId, int month, int year)
    {
        return await _readOnlyRepository.GetAnticipatedMonthlyTotal(companyId, month, year);
    }

    public async Task<Cart?> GetOpenCartByCompany(long companyId)
    {
        var cart = await _readOnlyRepository.GetOpenCartByCompany(companyId);
        cart.RemoveExpiredInvoices();
        return cart;
    }
}
