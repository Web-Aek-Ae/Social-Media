using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models.Database
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key for User
        [Required]
        public int UserId { get; set; }

        // Navigation property for User
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        // Foreign key for Post
        [Required]
        public int PostId { get; set; }

        // Navigation property for Post
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        // Optionally, include a ParentCommentId if you want to support threaded comments
        public int? ParentCommentId { get; set; }

        // Navigation property for parent Comment
        [ForeignKey("ParentCommentId")]
        public virtual Comment ParentComment { get; set; }

        // Collection of child Comments for threaded comments
        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}
