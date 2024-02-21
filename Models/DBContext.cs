using Microsoft.EntityFrameworkCore;

namespace Social_Media.Models
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
