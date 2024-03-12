
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.Database;



namespace SocialMedia.Services
{
    public class PostService
    {
        private readonly SocialMediaContext _context;

        public PostService(SocialMediaContext context)
        {
            _context = context;

        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.UserId == id) ?? throw new ArgumentException("User not found.");
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
       .Include(p => p.User)
       .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
       .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
       .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
       .Include(p => p.Category) // Include Category
       .Include(p => p.Group)
       .ToList();
        }
        public async Task<bool> MakePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
