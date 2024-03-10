using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Claims;
using SocialMedia.Models.Database;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {   
        
        private readonly UserService _userService;
        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Post()
        {
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _userService.GetUserById(int.Parse(UserId));
            return View(user);
        }

        public IActionResult Joined()
         {
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _userService.GetUserById(int.Parse(UserId));
            return View(user);
        }

        public IActionResult Likes()
         {
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _userService.GetUserById(int.Parse(UserId));
            return View(user);
        }

        public IActionResult Edit()
        {

            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _userService.GetUserById(int.Parse(UserId));
            return View(user);
        }
    }
}
