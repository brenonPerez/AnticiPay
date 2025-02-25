using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Domain.Security.Cryptography;
using AnticiPay.Domain.Security.Tokens;
using AnticiPay.Infrastructure.DataAccess;
using AnticiPay.Infrastructure.DataAccess.Repositories.Companies;
using AnticiPay.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnticiPay.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddToken(configuration);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyWriteOnlyRepository, CompanyRepository>();
        services.AddScoped<ICompanyReadOnlyRepository, CompanyRepository>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<AnticiPayDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

    private static void AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
}
