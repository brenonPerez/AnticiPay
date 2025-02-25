using AnticiPay.Domain.Repositories.Companies;
using AnticiPay.Infrastructure.DataAccess;
using AnticiPay.Infrastructure.DataAccess.Repositories.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnticiPay.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
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
}
