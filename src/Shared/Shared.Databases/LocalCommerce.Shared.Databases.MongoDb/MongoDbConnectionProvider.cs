using LocalCommerce.Shared.Databases.MongoDb;
using LocalCommerce.Shared.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocalCommerce.Shared.Databases.MongoDb;

public interface IMongoDbConnectionProvider
{
  MongoUrl GetMongoUrl();
  string GetMongoConnectionString();
}

public class MongoDbConnectionProvider : IMongoDbConnectionProvider
{
  private readonly ISecretManager _secretManager;
  private readonly IOptions<MongoDbConfiguration> _mongoDbConfiguration;

  private MongoUrl? MongoUrl { get; set; }
  private string? MongoConnectionString { get; set; }

  public MongoDbConnectionProvider(ISecretManager secretManager, IOptions<MongoDbConfiguration> mongoDbConfiguration)
  {
    _secretManager = secretManager;
    _mongoDbConfiguration = mongoDbConfiguration;
  }


  public MongoUrl GetMongoUrl()
  {
    if (MongoUrl is not null)
      return MongoUrl;

    MongoConnectionString = RetrieveMongoUrl().Result;
    MongoUrl = new MongoUrl(MongoConnectionString);

    return MongoUrl;
  }

  public string GetMongoConnectionString()
  {
    if (MongoConnectionString is null)
      GetMongoUrl();

    return MongoConnectionString ?? throw new Exception("Mongo connection string cannot be retrieved");
  }

  private async Task<string> RetrieveMongoUrl()
  {

    MongoDbCredentials credentials = await _secretManager.Get<MongoDbCredentials>("mongodb");

    return $"mongodb://{credentials.username}:{credentials.password}@{_mongoDbConfiguration.Value.DatabaseAddress}:{_mongoDbConfiguration.Value.DatabasePort}";
  }


  private record MongoDbCredentials
  {
    public string username { get; init; } = null!;
    public string password { get; init; } = null!;
  }
}