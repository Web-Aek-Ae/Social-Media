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
            var all_users = _userService.GetAllUsers();
            return View(all_users);
        }

        public IActionResult Register(){
            // User user = new User();
            return View();
        }

         [HttpPost]
        public ActionResult Register(User model)
        {
        if (ModelState.IsValid)
        {
            // Registration logic
            return RedirectToAction("Login", "User");
        }

        // If validation fails, return the view with validation errors
        return View(model);}

        public IActionResult Login(){
            return View();
        }
    }
}
