using AnticiPay.Communication.Requests;
using AnticiPay.Exception.Resources;
using FluentValidation;

namespace AnticiPay.Application.UseCases.Companies.Register;
public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanyJson>
{
    public RegisterCompanyValidator()
    {
        RuleFor(c => c.Cnpj)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.CNPJ_IS_REQUIRED)
            .Must(CnpjValidator.IsValid)
            .WithMessage(ResourceErrorMessages.INVALID_CNPJ);

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);

        RuleFor(c => c.MonthlyRevenue)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.MONTHLY_REVENUE_IS_REQUIRED)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.MONTHLY_REVENUE_GREATER_THAN_ZERO);

        RuleFor(c => c.BusinessType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.INVALID_BUSINESS_TYPE);

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_IS_REQUIRED)
            .EmailAddress()
            .When(c => string.IsNullOrEmpty(c.Email) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PASSWORD_IS_REQUIRED)
            .MinimumLength(6)
            .WithMessage(ResourceErrorMessages.INVALID_PASSWORD);
    }
}
