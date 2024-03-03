using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username must be alphanumeric")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        // Do not add MaxLength for password because it will be hashed
        public required string Password { get; set; }

    }
}
