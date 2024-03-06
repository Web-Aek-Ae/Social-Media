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

        public List<Group> GetAllGroups()
        {
            return _context.Groups.ToList();
        }
        public async Task<bool> AddGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            
            return true;
        }

        // public async Task<bool> JoinGroup(Group group, GroupMember groupMember)
        // {
        //     _context.Groups.Add(group,groupMember);
        //     await _context.SaveChangesAsync();
        //     return true;
        // }
    }
}