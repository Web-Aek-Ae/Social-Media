using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Namespace where Post is located
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SocialMedia.Controllers
{
    public class JoinActivityController : Controller
    {
        private readonly ILogger<JoinActivityController> _logger;
        private readonly JoinActivityService _joinActivityService;
        

        public JoinActivityController(ILogger<JoinActivityController> logger , JoinActivityService joinActivityService)
        {
            _logger = logger;
            _joinActivityService = joinActivityService;
        }

        [HttpPost]
        public async Task<IActionResult> JoinActivity(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            // Toggle like state
            var result = await _joinActivityService.ToggleActivity(postId, userId);

            return Json(result); // Assuming result contains success status and possibly new like count or state
        }






    }
}