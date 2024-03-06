using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

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
