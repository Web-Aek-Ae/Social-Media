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

        private readonly PostService _postService;

        public GroupService(SocialMediaContext context , PostService postService)
        {
            _context = context;
            _postService = postService;
        }
        

        public async Task<bool> DeleteGroup(int GroupId)
        {

            var group = await _context.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.GroupId == GroupId);
            if (group != null)
            {

                var post = await _context.Posts.Where(post => post.GroupId == GroupId).ToListAsync();

                if (post != null)
                {
                    foreach (var p in post)
                    {
                        await _postService.DeletePost(p.PostId);
                    }
                }


                if (group.Members != null)
                {
                    _context.GroupMembers.RemoveRange(group.Members);
                }

                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return true; 
            }
            return false;
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
            .Include(g => g.User).Where(g => g.Name == data)
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