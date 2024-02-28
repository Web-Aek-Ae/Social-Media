using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SocialMedia.Services
{
    public class UserService
    {
        private readonly SocialMediaContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(SocialMediaContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public async Task<bool> AddUser(User user)
        {

            // Check if username or email already exists
            bool usernameExists = _context.Users.Any(u => u.Username == user.Username);
            bool emailExists = _context.Users.Any(u => u.Email == user.Email);

            if (usernameExists || emailExists)
            {
                throw new ArgumentException("Username or email already in use.");
                // Or return false; depending on how you wish to handle this case
            }

            // Hash the password and add the user
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        // More methods for update, delete, etc.
    }
}
