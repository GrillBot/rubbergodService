using Microsoft.AspNetCore.Mvc;
using RubbergodService.Data.Managers;

namespace RubbergodService.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private UserManager UserManager { get; }

    public UserController(UserManager userManager)
    {
        UserManager = userManager;
    }

    [HttpPatch("{memberId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshUserAsync(string memberId)
    {
        await UserManager.InitMemberAndCommitAsync(memberId);
        return Ok();
    }
}
