using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Dtos;

namespace LocalCommerce.Services.Stores.Consumer.Handlers;

public class StoreCreatedHandler : IDomainMessageHandler<StoreCreated>
{
    private readonly IStoresReadStore _readStore;
    private readonly IIntegrationMessagePublisher _integrationMessagePublisher;

    public StoreCreatedHandler(IStoresReadStore readStore, IIntegrationMessagePublisher integrationMessagePublisher)
    {
        _readStore = readStore;
        _integrationMessagePublisher = integrationMessagePublisher;
    }


    public async Task Handle(DomainMessage<StoreCreated> message,
        CancellationToken cancelToken = default(CancellationToken))
    {
        await _readStore.UpsertStoreDetails(message.Content.Id, message.Content.StoreRequest.Details,
            cancelToken);

        // Se publica para los servicios externos
        await _integrationMessagePublisher.Publish(
            new StoreUpdated(message.Content.Id, message.Content.StoreRequest.Details), routingKey: "external",
            cancellationToken: cancelToken);
    }
}