using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Repositories.Carts;
using AnticiPay.Domain.Services.LoggedCompany;

namespace AnticiPay.Application.UseCases.Carts.GetCartClosedInfo;
public class GetCartClosedInfoUseCase : IGetCartClosedInfoUseCase
{
    private readonly ICartReadOnlyRepository _cartReadOnlyRepository;
    private readonly ILoggedCompany _loggedCompany;

    public GetCartClosedInfoUseCase(
        ICartReadOnlyRepository cartReadOnlyRepository,
        ILoggedCompany loggedCompany)
    {
        _cartReadOnlyRepository = cartReadOnlyRepository;
        _loggedCompany = loggedCompany;
    }

    public async Task<ResponseCartClosedInfo> Execute()
    {
        var loggedCompany = await _loggedCompany.Get();
        var currentMonth = DateTime.UtcNow.Month;
        var currentYear = DateTime.UtcNow.Year;
        var previousMonth = DateTime.UtcNow.AddMonths(-1).Month;
        var previousMonthYear = DateTime.UtcNow.AddMonths(-1).Year;

        var totalAnticipatedForMonth = await _cartReadOnlyRepository.GetAnticipatedMonthlyTotal(loggedCompany.Id, currentMonth, currentYear);
        var totalAnticipatedForPreviousMonth = await _cartReadOnlyRepository.GetAnticipatedMonthlyTotal(loggedCompany.Id, previousMonth, previousMonthYear);

        decimal comparisonPercentage = 0;
        if (totalAnticipatedForPreviousMonth.HasValue && totalAnticipatedForPreviousMonth > 0)
        {
            comparisonPercentage = ((totalAnticipatedForMonth ?? 0) - totalAnticipatedForPreviousMonth.Value) / totalAnticipatedForPreviousMonth.Value * 100;
        }

        return new ResponseCartClosedInfo
        {
            TotalAnticipatedForMonth = totalAnticipatedForMonth ?? 0,
            PreviousMonthComparisonPercentage = comparisonPercentage
        };
    }
}
