using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Models;
using webapi.Data;
using Microsoft.AspNetCore.Authentication;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;

  public LoginController(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IActionResult> LoginUser(Login user)
  {
    var findUser = _dbContext.User
      .Where(e => e.Email == user.Email && e.Password == user.Password)
      .FirstOrDefault();

    if (_dbContext.User != null)
    {
      if (findUser == null)
      {
        return BadRequest(new { message = "Invalid Credentials" });
      }
      else
      {
        List<Claim> claims = new List<Claim>()
        {
          new Claim(ClaimTypes.NameIdentifier, user.Email),
          new Claim(ClaimTypes.Name, user.Email)
        };
        ClaimsIdentity ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal cp = new ClaimsPrincipal(ci);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp);

        return StatusCode(200, new { message = "Login Successfully" });
      }
    }
    else
    {
      return BadRequest(new { message = "No Data in this database" });
    }

  }


}