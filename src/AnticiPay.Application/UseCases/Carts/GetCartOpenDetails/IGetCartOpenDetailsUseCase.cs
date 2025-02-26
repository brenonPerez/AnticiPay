using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Carts.GetCartOpenDetails;
public interface IGetCartOpenDetailsUseCase
{
    Task<ResponseCartDetailsJson> Execute();
}
