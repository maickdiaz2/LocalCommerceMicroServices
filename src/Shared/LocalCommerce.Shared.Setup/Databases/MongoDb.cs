using LocalCommerce.Shared.Databases.MongoDb;
using Microsoft.Extensions.Configuration;

namespace LocalCommerce.Shared.Setup.Databases;

public static class MongoDb
{
  public static IServiceCollection AddLocalCommerceMongoDbConnectionProvider(this IServiceCollection serviceCollection, IConfiguration configuration)
  {
    return serviceCollection
        .AddMongoDbDatabaseConfiguration(configuration)
        .AddMongoDbConnectionProvider();
  }
}