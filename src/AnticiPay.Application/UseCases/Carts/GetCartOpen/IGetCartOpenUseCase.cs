using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpen;
public interface IGetCartOpenUseCase
{
    Task<ResponseCartJson> Execute();
}
