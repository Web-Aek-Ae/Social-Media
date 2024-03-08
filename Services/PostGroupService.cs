using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace SocialMedia.Services
{
    public class PostGroupService
    {
        private readonly SocialMediaContext _context;


        public PostGroupService(SocialMediaContext context)
        {
            _context = context;

        }
        
        public List<PostGroup> GetAllPostsGroup()
        {
            return _context.PostGroups.Include(g => g.Group).Include(u => u.User).ToList();
        }



    }
}
