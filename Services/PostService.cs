
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.Database;
using SocialMedia.ViewModels;



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
        public List<Post> GetPostsByUsername(string data)
        {
            return _context.Posts
       .Include(p => p.User)
       .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
       .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
       .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
       .Include(p => p.Category) // Include Category
       .Include(p => p.Group)
       .Where(p => p.User.Username.Contains(data)) // Include Group
       .ToList();
        }
        public List<Post> GetPostsByCategory(string data)
        {
            return _context.Posts
       .Include(p => p.User)
       .Include(p => p.PostLikes).ThenInclude(pl => pl.User) // Include the User of each PostLike
       .Include(p => p.JoinActivities).ThenInclude(ja => ja.User) // Include the User of each JoinActivity
       .Include(p => p.Comments).ThenInclude(c => c.User) // Include Comments and their Users
       .Include(p => p.Category) // Include Category
       .Include(p => p.Group)
       .Where(p => p.Category.Name.Contains(data)) // Include Group
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

        // public async Task<bool> UpdatePost(PostViewModel post, int PostId)
        // {
        //     var existingPost = await _context.Posts.FindAsync(PostId);
        //     if (existingPost == null)
        //     {
        //         return false;
        //     }

        //     // existingPost.Category = post.Category;
        //     existingPost.Title = post.Title;
        //     existingPost.Content = post.Content;
        //     existingPost.Location = post.Location;
        //     existingPost.Image = post.Image;
        //     existingPost.MaxPeople = post.MaxPeople;
        //     // existingPost.Time = post.Time;
        //     existingPost.Date = post.Date;
        //     existingPost.ExpireDate = post.ExpireDate;

        //     await _context.SaveChangesAsync();
        //     return true;
        // }


        public async Task<bool> DeletePost(int id)
        {
            var post = _context.Posts.Include(p => p.Comments)
                              .Include(p => p.PostLikes)
                              .Include(p => p.JoinActivities)
                              .FirstOrDefault(p => p.PostId == id);

            if (post == null)
            {
                return false;
            }

            // If you have related entities to delete, do it here
            if (post.Comments != null)
            {
                _context.Comments.RemoveRange(post.Comments);
            }
            if (post.PostLikes != null)
            {
                _context.PostLikes.RemoveRange(post.PostLikes);
            }
            if (post.JoinActivities != null)
            {
                _context.JoinActivities.RemoveRange(post.JoinActivities);
            }

            // Now, delete the post
            _context.Posts.Remove(post);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
