using Microsoft.AspNetCore.Mvc;
using RubbergodService.Data.Entity;
using RubbergodService.Data.Managers;
using RubbergodService.Data.Models.Common;
using RubbergodService.Data.Models.Karma;

namespace RubbergodService.Controllers;

[ApiController]
[Route("api/karma")]
public class KarmaController : Controller
{
    private KarmaManager KarmaManager { get; }

    public KarmaController(KarmaManager manager)
    {
        KarmaManager = manager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> StoreKarmaAsync(List<Karma> items)
    {
        await KarmaManager.StoreKarmaAsync(items);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResponse<UserKarma>>> GetPageAsync([FromQuery] PaginatedParams parameters)
    {
        var result = await KarmaManager.GetPageAsync(parameters);
        return Ok(result);
    }
}
