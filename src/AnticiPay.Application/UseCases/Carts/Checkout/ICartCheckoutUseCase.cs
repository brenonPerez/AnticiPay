using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Carts.Checkout;
public interface ICartCheckoutUseCase
{
    Task<ResponseCartDetailsJson> Execute();
}
