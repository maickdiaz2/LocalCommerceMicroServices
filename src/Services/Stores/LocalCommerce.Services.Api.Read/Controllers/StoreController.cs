using LocalCommerce.Services.Stores.BusinessLogic.UseCases;
using LocalCommerce.Services.Stores.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LocalCommerce.Services.Stores.Api.Read.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class StoreController : Controller
  {
    private readonly IReadStoreDetails _readStoreDetails;

    public StoreController(IReadStoreDetails readStoreDetails)
    {
      _readStoreDetails = readStoreDetails;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultDto<FullStoreResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(int id)
    {
      var result = await _readStoreDetails.Execute(id);

      return result.Success().UseSuccessHttpStatusCode(HttpStatusCode.Created).ToActionResult();

    }

    [HttpPut("upsert/{id}")]
    [ProducesResponseType(typeof(ResultDto<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Upsert(int id, StoreDetails storeDetails)
    {
      var result = await _readStoreDetails.ExecuteUpsertStoreDetails(id, storeDetails);

      return result.Success().UseSuccessHttpStatusCode(HttpStatusCode.OK).ToActionResult();
    }
  }
}

