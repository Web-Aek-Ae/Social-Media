using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
        public IActionResult Joined()
        {
            return View();
        }
        public IActionResult Likes()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }

}