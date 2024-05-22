using LocalCommerce.Shared.Communication.Publisher.Domain;
using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Dtos;
using Microsoft.Extensions.Configuration;

namespace LocalCommerce.Services.Stores.BusinessLogic.UseCases;


public record CreateStoreResponse(string Url);


public interface ICreateStoreDetails
{
    Task<CreateStoreResponse> Execute(CreateStoreRequest storeRequest);
}

public class CreateStoreDetails : ICreateStoreDetails
{
    private readonly IStoresWriteStore _writeStore;
    private readonly IDomainMessagePublisher _domainMessagePublisher;
    private readonly IConfiguration _configuration;

    public CreateStoreDetails(IStoresWriteStore writeStore,
                              IConfiguration configuration,
                              IDomainMessagePublisher domainMessagePublisher)
    {
        _writeStore = writeStore;
        _configuration = configuration;
        _domainMessagePublisher = domainMessagePublisher;
    }


    public async Task<CreateStoreResponse> Execute(CreateStoreRequest storeRequest)
    {
        int storeId = await _writeStore.CreateRecord(storeRequest.Details);
        
        await _domainMessagePublisher.Publish(new StoreCreated(storeId, storeRequest), routingKey: "internal");
        
        string? getUrl = _configuration.GetSection("Services:StoreReadApi").Get<string>();

        return new CreateStoreResponse($"{getUrl}/store/{storeId}");
    }
}

