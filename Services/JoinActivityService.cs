using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace SocialMedia.Services
{
    public class JoinActivityService
    {
        private readonly SocialMediaContext _context;

        public JoinActivityService(SocialMediaContext context)
        {
            _context = context;
        }


        public async Task<object> ToggleActivity(int postId, string userId)
        {
            var postActivity = _context.JoinActivities.FirstOrDefault(pl => pl.PostId == postId && pl.UserId == int.Parse(userId));

            if (postActivity == null)
            {
                // Add like
                var joinActivity = new JoinActivity
                {
                    UserId = int.Parse(userId),
                    PostId = postId
                };
                _context.JoinActivities.Add(joinActivity);
                await _context.SaveChangesAsync();
                var count = await _context.JoinActivities.CountAsync(pl => pl.PostId == postId);
                return new { success = true, joined = true, JoinActivitiesCount = count };
            }
            else
            {
                _context.JoinActivities.Remove(postActivity);
                await _context.SaveChangesAsync();
                var count = await _context.JoinActivities.CountAsync(pl => pl.PostId == postId);
                return new { success = true, joined = false, JoinActivitiesCount = count };
            }
        }









    }


}