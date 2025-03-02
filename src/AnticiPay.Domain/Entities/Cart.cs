using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Services.Tax;

namespace AnticiPay.Domain.Entities;
public class Cart
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public decimal? TaxRateAtCheckout { get; set; }

    public Company Company { get; set; } = default!;

    public ICollection<Invoice> Invoices { get; set; } = [];

    public decimal TotalAmount => Invoices
        .Sum(ci => ci.Amount);

    public decimal CalculateTotalNetValue(ITaxService taxService) =>
        Invoices.Sum(invoice => invoice.CalculateNetValue(taxService));

    public CartStatus Status { get; set; } = CartStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
