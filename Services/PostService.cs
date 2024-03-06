using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



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
            return _context.Posts.ToList();
        }

        public async Task<bool> MakePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
