using SocialMedia.Models;
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

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // More methods for update, delete, etc.
    }
}
