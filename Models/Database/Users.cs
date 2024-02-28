using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.Database
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
         [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password {get; set;}

        }

}
