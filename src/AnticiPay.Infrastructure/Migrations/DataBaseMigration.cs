using AnticiPay.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnticiPay.Infrastructure.Migrations;
public class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<AnticiPayDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
