
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.Database;



namespace SocialMedia.Services
{
    public class PostLikeService
    {
        private readonly SocialMediaContext _context;

        public PostLikeService(SocialMediaContext context)
        {
            _context = context;

        }

        public async Task LikePost(int postId, string userId)
        {
            var postLike = new PostLike
            {
                PostId = postId,
                UserId = int.Parse(userId)
            };
            _context.PostLikes.Add(postLike);
            await _context.SaveChangesAsync();
        }

        public async Task<object> ToggleLike(int postId, string userId)
        {
            var postLike = _context.PostLikes.FirstOrDefault(pl => pl.PostId == postId && pl.UserId == int.Parse(userId));

            if (postLike == null)
            {
                // Add like
                var newLike = new PostLike { PostId = postId, UserId = int.Parse(userId) };
                _context.PostLikes.Add(newLike);
                await _context.SaveChangesAsync();
                var count =  await _context.PostLikes.CountAsync(pl => pl.PostId == postId);
                return new { success = true, liked = true  , LikeCount = count};
            } 
            else
            {
                // Remove like
                _context.PostLikes.Remove(postLike);
                await _context.SaveChangesAsync();
                var count =  await _context.PostLikes.CountAsync(pl => pl.PostId == postId);
                return new { success = true, liked = false  , LikeCount = count};
            }
        }





    }
}
