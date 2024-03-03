
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class Post
    {
        [Key] // Explicitly marks the property as a primary key
        public int PostId { get; set; }

        [ForeignKey("User")] // Specifies UserId as a foreign key referencing the User entity
        public int UserId { get; set; }
        
        public virtual User User { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }


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

        [DataType(DataType.Date)] // Specifies that the data type is a date, without a time component
        public DateTime Date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expire Date")]
        public DateTime ExpireDate { get; set; }

        // Additional properties or methods can be added here
    }
}
