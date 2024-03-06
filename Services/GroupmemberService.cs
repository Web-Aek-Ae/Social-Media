using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace SocialMedia.Services
{
    public class GroupmemberService
    {
        private readonly SocialMediaContext _context;

        public GroupmemberService(SocialMediaContext context)
        {
            _context = context;
        }


        public bool GetGroupMemberById(int? id)
        {
            var group = _context.Groups.FirstOrDefaultAsync(g => g.GroupId == id);
            if (group != null)
            {
                return true;
            } 
            return false;
        }
        public async Task<bool> JoinGroup(int id)
        {
            var group = _context.Groups.FirstOrDefaultAsync(g => g.GroupId == id);
            
            await _context.SaveChangesAsync();
            
            return true;
        }
    }

    
}