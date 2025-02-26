using AnticiPay.Domain.Services.Tax;

namespace AnticiPay.Domain.Entities;
public class Invoice
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; } = default!;

    public long? CartId { get; set; }
    public Cart? Cart { get; set; }

    public decimal? NetValueAtCheckout { get; set; }

    public decimal CalculateNetValue(ITaxService taxService)
    {
        return taxService.CalculateNetValue(Amount, DueDate);
    }
}
