using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class GroupController : Controller{
        public IActionResult Index()
        {
            return View();
        }
    }
    
}