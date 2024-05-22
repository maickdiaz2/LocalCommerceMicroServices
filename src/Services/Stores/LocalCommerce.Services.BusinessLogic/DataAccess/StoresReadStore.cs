using LocalCommerce.Services.Stores.Dtos;
using LocalCommerce.Shared.Databases.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LocalCommerce.Services.Stores.BusinessLogic.DataAccess
{
  public interface IStoresReadStore
  {
    Task<FullStoreResponse> GetStore(int id, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> UpsertStoreDetails(int id, StoreDetails details, CancellationToken cancellationToken = default(CancellationToken));
  }

  public class StoresReadStore : IStoresReadStore
  {

    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IOptions<MongoDbConfiguration> _mongoDbConfiguration;

    public StoresReadStore(IMongoDbConnectionProvider mongoDbConnectionProvider,
        IOptions<MongoDbConfiguration> mongoDbConfiguration)
    {
      _mongoDbConfiguration = mongoDbConfiguration;
      _mongoClient = new MongoClient(mongoDbConnectionProvider.GetMongoUrl());
      _mongoDatabase = _mongoClient.GetDatabase(mongoDbConfiguration.Value.DatabaseName);
    }

    public async Task<FullStoreResponse> GetStore(int id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
      IMongoCollection<FullStoreResponseEntity>
          collection = _mongoDatabase.GetCollection<FullStoreResponseEntity>(_mongoDbConfiguration.Value.DatabaseCollection);
      FilterDefinition<FullStoreResponseEntity> filter = Builders<FullStoreResponseEntity>.Filter.Eq("Id", id);
      FullStoreResponseEntity entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken) ?? new FullStoreResponseEntity();

      return entity.ToFullStoreResponse();
    }

    public async Task<bool> UpsertStoreDetails(int id, StoreDetails details,
        CancellationToken cancellationToken = default(CancellationToken))
    {
      IMongoCollection<FullStoreResponseEntity>
          collection = _mongoDatabase.GetCollection<FullStoreResponseEntity>(_mongoDbConfiguration.Value.DatabaseCollection);

      FilterDefinition<FullStoreResponseEntity> filter = Builders<FullStoreResponseEntity>.Filter.Eq("Id", id);

      FullStoreResponseEntity entity =
          await collection.Find(filter).FirstOrDefaultAsync(cancellationToken)
          ?? new FullStoreResponseEntity();

      entity.Id ??= id;
      entity.Details = details;
      //entity.Stock = 0; //default
      //entity.Price = 0; //Default

      var replaceOne = await collection.ReplaceOneAsync(filter,
          entity,
          new ReplaceOptions()
          {
            IsUpsert = true
          }, cancellationToken);

      return replaceOne.IsAcknowledged;
    }


    private class FullStoreResponseEntity
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string? _id { get; set; }

      public int? Id { get; set; }

      public StoreDetails? Details { get; set; }

      public FullStoreResponseEntity()
      {
        _id = ObjectId.GenerateNewId().ToString();
      }
      public FullStoreResponse ToFullStoreResponse()
      {
        Id ??= 0;
        Details ??= new StoreDetails("", "");

        return new FullStoreResponse(Id, Details);
      }
    }
  }
}
