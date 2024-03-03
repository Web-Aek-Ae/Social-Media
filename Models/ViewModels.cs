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
    public required string Name { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Please confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
  }

  public class PostViewModel
  {
    public string Name { get; set; }

    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public string Location { get; set; }

    public TimeOnly Time { get; set; }

    public string Image { get; set; }

    public int People { get; set; }
  }
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
  }

}
