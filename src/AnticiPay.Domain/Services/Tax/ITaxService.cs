namespace AnticiPay.Domain.Services.Tax;
public interface ITaxService
{
    decimal CalculateNetValue(decimal amount, DateTime dueDate);
}
