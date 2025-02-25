using AnticiPay.Communication.Requests;
using AnticiPay.Communication.Responses;
using AnticiPay.Domain.Entities;
using AutoMapper;

namespace AnticiPay.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<RequestRegisterCompanyJson, Company>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }
}
