using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Dtos;

namespace LocalCommerce.Services.Stores.BusinessLogic.UseCases;

public interface IUpdateStoreDetails
{
    Task<bool> Execute(int id, StoreDetails storeDetails);
}

public class UpdateStoreDetails : IUpdateStoreDetails
{
    private readonly IStoresWriteStore _writeStore;
    //private readonly IDomainMessagePublisher _domainMessagePublisher;

    public UpdateStoreDetails(IStoresWriteStore writeStore)//, IDomainMessagePublisher domainMessagePublisher)
    {
        _writeStore = writeStore;
        //_domainMessagePublisher = domainMessagePublisher;
    }

    public async Task<bool> Execute(int id, StoreDetails storeDetails)
    {
        await _writeStore.UpdateStore(id, storeDetails);

        //await _domainMessagePublisher.Publish(new StoreUpdated(id, storeDetails), routingKey: "internal");

        return true;
    }
}