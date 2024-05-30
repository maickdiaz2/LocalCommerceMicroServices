using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Dtos;

namespace LocalCommerce.Services.Stores.Consumer.Handlers;

public class StoreUpdatedHandler : IDomainMessageHandler<StoreUpdated>
{
    private readonly IStoresReadStore _readStore;
    private readonly IIntegrationMessagePublisher _integrationMessagePublisher;

    public StoreUpdatedHandler(IStoresReadStore readStore, IIntegrationMessagePublisher integrationMessagePublisher)
    {
        _readStore = readStore;
        _integrationMessagePublisher = integrationMessagePublisher;
    }

    public async Task Handle(DomainMessage<StoreUpdated> message, CancellationToken cancelToken = default(CancellationToken))
    {

        await _readStore.UpsertStoreDetails(message.Content.StoreId, message.Content.Details, cancelToken);

        await _integrationMessagePublisher.Publish(
            new StoreUpdated(message.Content.StoreId, message.Content.Details), routingKey:"external", cancellationToken: cancelToken);
    }
}