using Microsoft.AspNetCore.Mvc;
using Models;
using webapi.Data;

//using webapi.Models;

namespace webapi.Controllers
{
  [Route("login/[controller]")]
  [ApiController]
  public class ForgotPasswordController : ControllerBase
  {

    private readonly ApplicationDbContext _dbContext;
    public ForgotPasswordController(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpPut]
    public async Task<IActionResult> ResetPassword(ForgetPassword reset)
    {
      var resetPass = _dbContext.User
        .Where(e => e.Email == reset.Email && e.PasswordReset == reset.PetName)
        .FirstOrDefault();

      if (resetPass != null)
      {
        if (reset.Password == reset.ConfirmationPassword)
        {
          resetPass.Password = reset.Password;
          await _dbContext.SaveChangesAsync();
        }
        else
        {
          return BadRequest("Your new password is not the same with you confirmation password");
        }

      }
      else
      {
        return BadRequest("Pet name or Email is not correct");
      }
      return StatusCode(200, new { message = "Reset Password Successful" });
    }

  }
}
