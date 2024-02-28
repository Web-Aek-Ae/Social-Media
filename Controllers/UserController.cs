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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = new User
                {
                    Username = model.Username,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password // This should be hashed inside AddUser
                };

                var result = await _userService.AddUser(user);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Handle false result if you chose to return a boolean from AddUser
            }
            catch (ArgumentException ex)
            {
                // Log the exception or set model state error
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
