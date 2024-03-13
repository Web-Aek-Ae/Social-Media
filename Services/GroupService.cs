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
    public class GroupService
    {
        private readonly SocialMediaContext _context;

        public GroupService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteGroup(int GroupId)
        {
            var group = await _context.Groups.FindAsync(GroupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return true; // Or use TempData or another way to communicate success
            }
            return false; // Or communicate the user was not found
        }
        public List<Group> GetAllGroups()
        {
            return _context.Groups.Include(g => g.Members).ThenInclude(gl => gl.User)
            .Include(g => g.User)
            .ToList();
        }
        public List<Group> GetGroupsByName(string data)
        {
            return _context.Groups.Include(g => g.Members).ThenInclude(gl => gl.User)
            .Include(g => g.User).Where(g=>g.Name.ToLower().Contains(data.ToLower()))
            .ToList();
        }

        public Group? GetGroupById(int? id)
        {
            return _context.Groups.Include(g => g.Members).Include(g => g.User).FirstOrDefault(g => g.GroupId == id);
        }

        public async Task<Group?> GetGroupByIdAsync(int? id)
        {
            return await _context.Groups.Include(g => g.Members).Include(g => g.User).FirstOrDefaultAsync(g => g.GroupId == id);
        }

        public async Task<bool> AddGroup(Group group)
        {
            var newGroup = _context.Groups.Add(group).Entity;
            await _context.SaveChangesAsync();

            var groupMember = new GroupMember
            {
                GroupId = newGroup.GroupId,
                UserId = group.UserId // Assuming you want to use the UserId from the passed group object
            };

            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return true;


        }

    }
}