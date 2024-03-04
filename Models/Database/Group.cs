using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.Database
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The group name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for the group's members
        public virtual ICollection<GroupMember> Members { get; set; }
        
    }
}
