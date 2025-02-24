using AnticiPay.Application.UseCases.Companies.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AnticiPay.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
    }
}
