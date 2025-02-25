using AnticiPay.Communication.Requests;
using FluentValidation;

namespace AnticiPay.Application.UseCases.Invoices.Register;
public class RegisterInvoiceValidator : AbstractValidator<RequestInvoiceJson>
{
    public RegisterInvoiceValidator()
    {
        RuleFor(i => i.Number)
            .NotEmpty()
            .WithMessage("Number is required")
            .Matches(@"^\d+$")
            .WithMessage("Number must contain only digits");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

        RuleFor(x => x.DueDate.Date)
            .GreaterThan(DateTime.UtcNow.Date)
            .WithMessage("The due date must be in the future");
    }
}
