using AnticiPay.Communication.Requests;
using FluentValidation;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanyJson>
{
    public RegisterCompanyValidator()
    {
        RuleFor(c => c.Cnpj)
            .NotEmpty()
            .WithMessage("CNPJ is required")
            .Must(CnpjValidator.IsValid)
            .WithMessage("Invalid CNPJ");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(c => c.MonthlyRevenue)
            .GreaterThan(0)
            .WithMessage("MonthlyRevenue must be greater than 0");

        RuleFor(c => c.BusinessType)
            .IsInEnum()
            .WithMessage("Invalid BusinessType");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .When(c => string.IsNullOrEmpty(c.Email) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("Invalid Email");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
    }
}
