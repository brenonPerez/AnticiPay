using AnticiPay.Domain.Entities;
using AnticiPay.Domain.Security.Tokens;
using AnticiPay.Domain.Services.LoggedCompany;
using AnticiPay.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnticiPay.Infrastructure.Services.LoggedCompany;
internal class LoggedCompany : ILoggedCompany
{
    private readonly AnticiPayDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedCompany(AnticiPayDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<Company> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identitierCompany = jwtSecurityToken
            .Claims
            .First(claim => claim.Type.Equals(ClaimTypes.Sid))
            .Value;

        return await _dbContext
            .Companies
            .AsNoTracking()
            .FirstAsync(c => c.CompanyIdentifier == Guid.Parse(identitierCompany));
    }
}
