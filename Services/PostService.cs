
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
        
        public List<Post> GetAllPosts()
        {
            return _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostLikes) // Include PostLikes
        .Include(p => p.JoinActivities) // Include JoinActivities
        .Include(p => p.Group)
        .ToList();
        }
        
        public List<Post> GetPostsByGroupId(int id)
        {
            return _context.Posts.Where(p => p.GroupId == id).Include(p => p.Group).Include(p => p.User).Include(p => p.PostLikes).Include(p => p.JoinActivities).ToList();
        }
        public async Task<bool> MakePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
