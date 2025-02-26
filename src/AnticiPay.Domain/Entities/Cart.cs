using AnticiPay.Domain.Enums;
using AnticiPay.Domain.Services.Tax;

namespace AnticiPay.Domain.Entities;
public class Cart
{
    public long Id { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; } = default!;

    public ICollection<Invoice> Invoices { get; set; } = [];

    public decimal TotalAmount => Invoices
        .Where(ci => ci.DueDate.Date > DateTime.UtcNow.Date)
        .Sum(ci => ci.Amount);

    public CartStatus Status { get; set; } = CartStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool ExceedsCreditLimit => TotalAmount > Company.GetCreditLimit();
}
