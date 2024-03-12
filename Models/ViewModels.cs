using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SocialMedia.ViewModels
{
  public class TableViewModel
  {
    public List<string> TableNames { get; set; } = new List<string>(); // Initialized to prevent null reference
  }
  public class UserViewModel
  {


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

  public class LoginViewModel
  {
    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
  }

  public class GroupViewModel
  {

    [Required(ErrorMessage = "Please enter name")]
    public required string Groupname { get; set; }

    public required string Description { get; set; }

    public required string Image { get; set; }
  }
  public class PostViewModel
  {
    [Required(ErrorMessage = "Title is required")]
    [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")] // Limit title length
    public required string Title { get; set; }

    [Required(ErrorMessage = "Content is required")]
    [DataType(DataType.MultilineText)] // Specifies that the data type is a multiline text, useful for UI hints
    public required string Content { get; set; }

    [StringLength(500, ErrorMessage = "Location cannot be longer than 500 characters.")] // Optionally limit location length
    public string? Location { get; set; }

    public string? Image { get; set; } // Consider validating the image URL or path if applicable

    [Range(1, int.MaxValue, ErrorMessage = "MaxPeople must be at least 1")] // Ensures MaxPeople is a positive number
    public int MaxPeople { get; set; }

    public string Time { get; set; } // Change to string if parsing server-side


    [DataType(DataType.Date)] // Specifies that the data type is a date, without a time component
    public DateTime Date { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Expire Date")]
    public DateTime ExpireDate { get; set; }

    public int SelectedCategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
  }

  public class CategoryViewModel
  {
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
    public required string Name { get; set; }
  }

  public class JoinActivityViewModel
  {
    public int PostId { get; set; }
  }

  public class EditProfileViewModel
  {
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    public string? Image { get; set; }
  }

  public class EditImageViewModel
{
    [Required]
    [Url]
    public string Image { get; set; }
}


}
