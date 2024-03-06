using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SocialMedia.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var specificClaimUsername = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Use the username for your application logic...
            ViewData["Username"] = username;

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Recommend()
        {
            return View();
        }
    }
    

}