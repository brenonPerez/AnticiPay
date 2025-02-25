using AnticiPay.Communication.Requests;
using AnticiPay.Exception.Resources;
using FluentValidation;

namespace AnticiPay.Application.UseCases.Invoices.Register;
public class RegisterInvoiceValidator : AbstractValidator<RequestInvoiceJson>
{
    public RegisterInvoiceValidator()
    {
        RuleFor(i => i.Number)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NUMBER_IS_REQUIRED)
            .Matches(@"^\d+$")
            .WithMessage(ResourceErrorMessages.NUMBER_MUST_CONTAIN_ONLY_DIGITS);

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(x => x.DueDate.Date)
            .GreaterThan(DateTime.UtcNow.Date)
            .WithMessage(ResourceErrorMessages.DUE_DATE_MUST_BE_IN_THE_FUTURE);
    }
}
