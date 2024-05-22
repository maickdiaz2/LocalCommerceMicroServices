using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.BusinessLogic.UseCases;


var app = DefaultLocalCommerceWebApplication.Create(args, builder =>
{
  builder.Services.AddLocalCommerceMongoDbConnectionProvider(builder.Configuration)
                  .AddScoped<IStoresReadStore, StoresReadStore>()
                  .AddScoped<IReadStoreDetails, ReadStoreDetails>();

});

DefaultLocalCommerceWebApplication.Run(app);