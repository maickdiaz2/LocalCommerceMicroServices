using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.BusinessLogic.UseCases;

var app = DefaultLocalCommerceWebApplication.Create(args, builder =>
{
    builder.Services.AddMssql<StoresWriteStore>("LocalCommerce")
                    .AddScoped<IStoresWriteStore, StoresWriteStore>()
                    .AddScoped<IUpdateStoreDetails, UpdateStoreDetails>()
                    .AddScoped<ICreateStoreDetails, CreateStoreDetails>()
                    .AddServiceBusDomainPublisher(builder.Configuration);
});

DefaultLocalCommerceWebApplication.Run(app);