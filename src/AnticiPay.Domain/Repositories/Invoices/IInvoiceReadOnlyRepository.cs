﻿using AnticiPay.Domain.Entities;

namespace AnticiPay.Domain.Repositories.Invoices;
public interface IInvoiceReadOnlyRepository
{
    Task<bool> ExistInvoiceWithNumber(string number);

    Task<List<Invoice>> GetAllNotInCartByCompany(long companyId);

    Task<List<Invoice>> GetAllByCompany(long companyId, string? number, decimal? amount, DateTime? dueDate, int pageIndex, int pageSize);

    Task<long> GetTotalCountByCompany(long companyId);
}
