using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Consumer.Handlers;

WebApplication app = DefaultLocalCommerceWebApplication.Create(args, builder =>
{
  builder.Services.AddLocalCommerceMongoDbConnectionProvider(builder.Configuration)
      .AddScoped<IStoresReadStore, StoresReadStore>()
      .AddServiceBusIntegrationPublisher(builder.Configuration)
      .AddHandlersInAssembly<StoreUpdatedHandler>()
      .AddServiceBusDomainConsumer(builder.Configuration);
});


DefaultLocalCommerceWebApplication.Run(app);