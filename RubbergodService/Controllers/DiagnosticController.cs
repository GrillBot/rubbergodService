using Microsoft.AspNetCore.Mvc;
using RubbergodService.Data.Managers;
using RubbergodService.Data.Models.Diagnostics;

namespace RubbergodService.Controllers;

[ApiController]
[Route("api/diag")]
public class DiagnosticController : Controller
{
    private DiagnosticManager DiagnosticManager { get; }

    public DiagnosticController(DiagnosticManager diagnosticManager)
    {
        DiagnosticManager = diagnosticManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DiagnosticInfo>> GetInfoAsync()
    {
        var result = await DiagnosticManager.GetInfoAsync();
        return Ok(result);
    }
}
