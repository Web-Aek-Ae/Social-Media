using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SocialMedia.Controllers
{
    [Authorize]
    public class GroupController : Controller{
        public IActionResult Index()
        {
            return View();
        }
    }
    
}