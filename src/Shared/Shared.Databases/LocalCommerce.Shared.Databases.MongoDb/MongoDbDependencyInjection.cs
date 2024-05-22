using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;

namespace LocalCommerce.Shared.Databases.MongoDb;

public static class MongoDbDependencyInjection
{
  public static IServiceCollection AddMongoDbDatabaseConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
  {
    serviceCollection.Configure<MongoDbConfiguration>(configuration.GetSection("Database:MongoDb"));
    return serviceCollection;
  }

  public static IServiceCollection AddMongoDbConnectionProvider(this IServiceCollection serviceCollection)
  {
    var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
    ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);

    return serviceCollection.AddScoped<IMongoDbConnectionProvider, MongoDbConnectionProvider>();
  }
}

public class MongoDbConfiguration
{
  public string DatabaseAddress { get; set; } = default!;
  public string DatabasePort { get; set; } = default!;
  public string DatabaseName { get; set; } = default!;
  public string DatabaseCollection { get; set; } = default!;
}

public class MongoEventStoreConfiguration
{
  public string DatabaseName { get; set; } = default!;
  public string CollectionName { get; set; } = default!;
}