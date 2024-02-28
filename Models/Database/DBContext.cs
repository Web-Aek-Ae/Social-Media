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
        // DbSets for other models...
    }
}
