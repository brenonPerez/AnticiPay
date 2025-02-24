using AnticiPay.Application.AutoMapper;
using AnticiPay.Application.UseCases.Companies.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AnticiPay.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
