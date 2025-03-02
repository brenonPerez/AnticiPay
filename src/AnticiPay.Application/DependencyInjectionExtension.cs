using AnticiPay.Application.AutoMapper;
using AnticiPay.Application.UseCases.Carts.AddInvoice;
using AnticiPay.Application.UseCases.Carts.Checkout;
using AnticiPay.Application.UseCases.Carts.GetCartClosedInfo;
using AnticiPay.Application.UseCases.Carts.GetCartOpen;
using AnticiPay.Application.UseCases.Carts.GetCartOpenDetails;
using AnticiPay.Application.UseCases.Carts.RemoveInvoice;
using AnticiPay.Application.UseCases.Companies.Register;
using AnticiPay.Application.UseCases.Invoices.GetAll;
using AnticiPay.Application.UseCases.Invoices.GetAllNotInCart;
using AnticiPay.Application.UseCases.Invoices.Register;
using AnticiPay.Application.UseCases.Login.DoLogin;
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
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IRegisterInvoiceUseCase, RegisterInvoiceUseCase>();
        services.AddScoped<IAddInvoiceToCartUseCase, AddInvoiceToCartUseCase>();
        services.AddScoped<IRemoveInvoiceFromCartUseCase, RemoveInvoiceFromCartUseCase>();
        services.AddScoped<IGetAllNotInCartInvoicesUseCase, GetAllNotInCartInvoicesUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetCartOpenUseCase, GetCartOpenUseCase>();
        services.AddScoped<IGetCartOpenDetailsUseCase, GetCartOpenDetailsUseCase>();
        services.AddScoped<ICartCheckoutUseCase, CartCheckoutUseCase>();
        services.AddScoped<IGetCartClosedInfoUseCase, GetCartClosedInfoUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
