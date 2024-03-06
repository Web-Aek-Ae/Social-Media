using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        

        [Required]
        [StringLength(100, ErrorMessage = "The group name cannot exceed 100 characters.")]
        public string Name { get; set; }
    }
}
