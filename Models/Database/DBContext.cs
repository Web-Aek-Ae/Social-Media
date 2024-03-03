using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Models.Database
{
    public class SocialMediaContext : DbContext
    {
        public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationship between Posts and Users
            modelBuilder.Entity<Post>()
                .HasOne<User>(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            // Configuring the many-to-many relationship between Users and Posts through PostLikes
            modelBuilder.Entity<PostLike>()
                .HasKey(pl => new { pl.UserId, pl.PostId }); // Composite key

            modelBuilder.Entity<PostLike>()
                .HasOne<Post>(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId);

            modelBuilder.Entity<PostLike>()
                .HasOne<User>(pl => pl.User)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.UserId);

            // Add any additional model configuration here
        }
    }
}
