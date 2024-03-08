using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class JoinActivity
    {
        [Key]
        public int JoinActivityId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int PostId { get; set; }
        
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        


    }
}
