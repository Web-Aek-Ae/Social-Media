using System.ComponentModel.DataAnnotations;


namespace SocialMedia.ViewModels
{
  public class TableViewModel
  {
    public List<string> TableNames { get; set; } = new List<string>(); // Initialized to prevent null reference
  }
  public class UserViewModel
  {
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }

}
