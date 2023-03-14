using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class LogoutController : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;
  public LogoutController(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IActionResult> LogoutUser()
  {
    await HttpContext.SignOutAsync();
    return StatusCode(200, new { message = "Logout Successfully" });
  }

}
