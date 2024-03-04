using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var specificClaimUsername = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            ViewData["Username"] = username;
            return View();
        }
    }

}