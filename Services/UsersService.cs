using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;

namespace SocialMedia.Services
{
    public class UserService
    {
        private readonly SocialMediaContext _context;

        public UserService(SocialMediaContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


        // More methods for update, delete, etc.
    }
}
