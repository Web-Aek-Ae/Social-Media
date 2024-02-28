using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Assuming this is where your User entity is defined
using SocialMedia.ViewModels; // Reference UserViewModel
using System.Threading.Tasks;


namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var all_users = _userService.GetAllUsers();
            return View(all_users);
        }

        public IActionResult Register()
        {
            // User user = new User();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password // Remember to hash the password in a real application
                };

                await _userService.AddUser(user);
                return RedirectToAction("Login", "User");
            }

            // If validation fails, return the view with validation errors
            return View(model);
        }
        
        public IActionResult Login()
        {
            return View();
        }
    }
}
