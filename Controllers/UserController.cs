using Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

  private readonly ApplicationDbContext _dbContext;

  public UserController(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  [ServiceFilter(typeof(AuthenticationFiltering))]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
  {
    var userList = _dbContext.User.ToList();
    if (userList == null)
    {
      return Ok(new { message = "still no user" });
    }
    return await Task.FromResult(_dbContext.User.ToList());
  }

  [ServiceFilter(typeof(AuthenticationFiltering))]
  [HttpGet("{id}")]
  public async Task<ActionResult<UserModel>> GetUser(String id)
  {
    var user = await _dbContext.User.FindAsync(id);
    if (user == null)
    {
      return NotFound();
    }

    return user;
  }

  [HttpPost]
  public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
  {
    try
    {
      _dbContext.User.Add(user);
      await _dbContext.SaveChangesAsync();
      return Ok(new { message = "Success" });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new { result = ex.Message, message = "fail" });
    }
  }

  [ServiceFilter(typeof(AuthenticationFiltering))]
  [HttpPut("{id}")]
  public async Task<IActionResult> EditUser(string id, UserModel user)
  {
    if (id != user.Id)
    {
      return BadRequest();
    }

    _dbContext.Entry(user).State = EntityState.Modified;

    try
    {
      await _dbContext.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!UserExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();

  }

  [ServiceFilter(typeof(AuthenticationFiltering))]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(string id)
  {
    var user = await _dbContext.User.FindAsync(id);
    if (user == null)
    {
      return NotFound();
    }

    _dbContext.User.Remove(user);
    await _dbContext.SaveChangesAsync();

    return NoContent();
  }

  private bool UserExists(String id)
  {
    return _dbContext.User.Any(e => e.Id == id);
  }
}