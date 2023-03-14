using System.ComponentModel.DataAnnotations;

namespace webapi.Models;

public class UserModel
{
  [Key]
  public string? Id { get; set; }
  [Required(ErrorMessage = "First Name is required")]
  public string? FirstName { get; set; }
  [Required(ErrorMessage = "Last Name is required")]
  public string? LastName { get; set; }
  [Required(ErrorMessage = "Email is required")]
  public string? Email { get; set; }
  [Required(ErrorMessage = "Password is required")]
  public string? Password { get; set; }
  [Required(ErrorMessage = "Position is required")]
  public string? Postition { get; set; }
  [Required(ErrorMessage = "Password reset is required")]
  public string? PasswordReset { get; set; }
}