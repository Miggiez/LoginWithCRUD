using System.ComponentModel.DataAnnotations;

namespace Models;

public class Login
{
  [Required(ErrorMessage = "Email is required")]
  public string? Email { get; set; }

  [Required(ErrorMessage = "Password is required")]
  public string? Password { get; set; }
}