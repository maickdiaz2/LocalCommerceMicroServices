using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LocalCommerce.Shared.Databases.Mssql;

public static class MssqlDependencyInjection
{
    public static IServiceCollection AddMssqlDbContext<T>(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
        where T : DbContext
    {
        return serviceCollection.AddDbContext<T>((serviceProvider, builder) =>
        {
            builder.UseSqlServer(connectionString.Invoke(serviceProvider).Result);
        });
    }
}