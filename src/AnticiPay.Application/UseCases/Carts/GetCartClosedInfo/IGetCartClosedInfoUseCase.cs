using AnticiPay.Communication.Responses;

namespace AnticiPay.Application.UseCases.Carts.GetCartClosedInfo;
public interface IGetCartClosedInfoUseCase
{
    Task<ResponseCartClosedInfo> Execute();
}
