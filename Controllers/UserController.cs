using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models;

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
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // Other actions...
    }
}