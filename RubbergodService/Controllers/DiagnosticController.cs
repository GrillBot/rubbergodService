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
    public ActionResult<DiagnosticInfo> GetInfo()
    {
        var result = DiagnosticManager.GetInfo();
        return Ok(result);
    }
}
