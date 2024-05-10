using LocalCommerce.Shared.Databases.Mssql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LocalCommerce.Shared.Setup.Databases;

public static class Mssql
{
    public static IServiceCollection AddMssql<T>(this IServiceCollection serviceCollection, string databaseName)
        where T : DbContext
    {
        return serviceCollection
            .AddMssqlDbContext<T>(serviceProvider => GetConnectionString(serviceProvider, databaseName));
            //.AddMysqlHealthCheck(serviceProvider => GetConnectionString(serviceProvider, databaseName));
    }

    private static async Task<string> GetConnectionString(IServiceProvider serviceProvider, string databaseName)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        ISecretManager secretManager = serviceProvider.GetRequiredService<ISecretManager>();
        //IServiceDiscovery serviceDiscovery = serviceProvider.GetRequiredService<IServiceDiscovery>();

        //DiscoveryData mysqlData = await serviceDiscovery.GetDiscoveryData(DiscoveryServices.MySql);

        MssqlCredentials credentials = await secretManager.Get<MssqlCredentials>("mssql");

        var mssqlDbConnectionString = configuration.GetConnectionString("mssqlDb") ?? "";

        mssqlDbConnectionString = mssqlDbConnectionString
                                    .Replace("GetUserFromVault", credentials.username)
                                    .Replace("GetPasswordFromVault", credentials.password)
                                    .Replace("DatabaseName", databaseName);

        return mssqlDbConnectionString;
            //$"Server={mssqlData.Server};Port={mysqlData.Port};Database={databaseName};Uid={credentials.username};password={credentials.password};";
    }

    public class MssqlCredentials
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    private record MssqlCredentials2
    {
        public string username { get; init; } = null!;
        public string password { get; init; } = null!;
    }
}