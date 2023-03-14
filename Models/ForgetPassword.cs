using System.ComponentModel.DataAnnotations;

namespace Models;

public class ForgetPassword
{
  [Required(ErrorMessage = "Emaill is required")]
  public string? Email { get; set; }
  [Required(ErrorMessage = "PetName is required")]
  public string? PetName { get; set; }
  [Required(ErrorMessage = "New Password is required")]
  public string? Password { get; set; }
  [Required(ErrorMessage = "Confirmation Password is required")]
  public string? ConfirmationPassword { get; set; }
}
