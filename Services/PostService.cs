
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

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
                .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
                .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
                .Include(p => p.Category) // Include Category
                .Include(p => p.Group) // Include Group
                .ToListAsync();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
       .Include(p => p.User)
       .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
       .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
       .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
       .Include(p => p.Category) // Include Category
       .Include(p => p.Group) // Include Group
       .ToList();
        }
        public List<Post> GetPostsByTitle(string data)
        {
            return _context.Posts
       .Include(p => p.User)
       .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
       .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
       .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
       .Include(p => p.Category) // Include Category
       .Include(p => p.Group)
       .Where(p => p.Title.Contains(data)) // Include Group
       .ToList();
        }

        public Post? GetPostByPostId(int id)
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
                .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
                .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
                .Include(p => p.Category) // Include Category
                .Include(p => p.Group)
                .FirstOrDefault(p => p.PostId == id); // Include Group;
        }

        public List<Post> GetPostsByGroupId(int id)
        {
            return _context.Posts
            .Include(p => p.Group)
            .Include(p => p.User)
            .Include(p => p.PostLikes).ThenInclude(pl => pl.User)
            .Include(p => p.JoinActivities)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Category)
            .Where(p => p.GroupId == id).ToList();
        }

        public async Task<List<Post>> GetPostsByGroupIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Group)
                .Include(p => p.User)
                .Include(p => p.PostLikes).ThenInclude(pl => pl.User)
                .Include(p => p.JoinActivities)
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .Include(p => p.Category)
                .Where(p => p.GroupId == id).ToListAsync();
        }
        public async Task<bool> MakePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
