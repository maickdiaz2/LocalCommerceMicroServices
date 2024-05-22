using LocalCommerce.Services.Stores.BusinessLogic.DataAccess;
using LocalCommerce.Services.Stores.Dtos;

namespace LocalCommerce.Services.Stores.BusinessLogic.UseCases
{
  public interface IReadStoreDetails
  {
    Task<FullStoreResponse> Execute(int id);
  
    Task<bool> ExecuteUpsertStoreDetails(int id, StoreDetails storeDetails);

  }

  public class ReadStoreDetails : IReadStoreDetails
  {
    private readonly IStoresReadStore _storesReadStore;

    public ReadStoreDetails(IStoresReadStore storesReadStore)
    {
      _storesReadStore = storesReadStore;
    }


    public async Task<FullStoreResponse> Execute(int id)
    {
      var response = await _storesReadStore.GetStore(id);

      return response;
    }

    public async Task<bool> ExecuteUpsertStoreDetails(int id, StoreDetails storeDetails)
    {
      var result = await _storesReadStore.UpsertStoreDetails(id, storeDetails);

      return result;
    }
  }
}
