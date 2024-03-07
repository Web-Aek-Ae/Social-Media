using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class GroupMember
    {
        [Key]
        public int GroupMemberId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int GroupId { get; set; }
        
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        
        // Additional properties like member role within the group could be added here
    }
}
