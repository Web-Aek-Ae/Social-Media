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
        public async Task<bool> JoinGroup(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public List<GroupMember>? GetAllGroupMembersForUser(int userId)
        {
            // Retrieve the user from the database including the GroupMembers navigation property
            var userWithGroupMembers = _context.Users.Include(u => u.GroupMembers).FirstOrDefault(u => u.UserId == userId);

            // If the user exists
            if (userWithGroupMembers != null)
            {
                // Return the GroupMembers associated with the user
                return userWithGroupMembers.GroupMembers.ToList();
            }
            return null;
        }
    }

    
}