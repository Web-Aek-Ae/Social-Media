using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SocialMedia.Services
{
    public class CommentService
    {
        private readonly SocialMediaContext _context;

        public CommentService(SocialMediaContext context)
        {
            _context = context;
        }

       public async Task<bool> AddComment(Comment comment){
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return true;
       }

       public List<Comment> GetCommentsByPostId(int id){
        return _context.Comments.Include(c => c.User).Where(c => c.PostId == id).OrderBy(c => c.CreatedAt).ToList();
       }
    }
}
