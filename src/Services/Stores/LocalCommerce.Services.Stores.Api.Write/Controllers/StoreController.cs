using System.Net;
using LocalCommerce.Services.Stores.BusinessLogic.UseCases;
using LocalCommerce.Services.Stores.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LocalCommerce.Services.Stores.Api.Write.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController
{

    private readonly IUpdateStoreDetails _updateStoreDetails;
    private readonly ICreateStoreDetails _createStoreDetails;

    public StoreController(ICreateStoreDetails createStoreDetails, IUpdateStoreDetails updateStoreDetails)
    {
        _createStoreDetails = createStoreDetails;
        _updateStoreDetails = updateStoreDetails;

    }

    [HttpPost(Name = "addStore")]
    [ProducesResponseType(typeof(ResultDto<CreateStoreResponse>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddStore(CreateStoreRequest createStoreRequest)
    {
        CreateStoreResponse result = await _createStoreDetails.Execute(createStoreRequest);

        return result.Success().UseSuccessHttpStatusCode(HttpStatusCode.Created).ToActionResult();
    }


    [HttpPut("updateStoredetails/{id}")]
    [ProducesResponseType(typeof(ResultDto<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateStoreDetails(int id, StoreDetails StoreDetails)
    {
        bool result = await _updateStoreDetails.Execute(id, StoreDetails);

        return result.Success().UseSuccessHttpStatusCode(HttpStatusCode.OK).ToActionResult();
    }
}