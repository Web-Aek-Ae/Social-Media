using Microsoft.AspNetCore.Identity;
using SocialMedia.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.ViewModels;



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

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true; // Or use TempData or another way to communicate success
            }
            return false; // Or communicate the user was not found
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Posts).Include(u => u.JoinActivities).ThenInclude(ja=>ja.Post).FirstOrDefault(u => u.UserId == id) ?? throw new ArgumentException("User not found.");
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null!;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return verificationResult == PasswordVerificationResult.Success ? user : null!;
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

        public async Task<bool> UpdateUser(EditProfileViewModel user  , int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
               return false;
            }

            existingUser.Name = user.Name;
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
