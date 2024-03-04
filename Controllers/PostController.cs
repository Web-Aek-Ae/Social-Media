using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Namespace where Post is located
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using System.Security.Claims;



namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var username = HttpContext.User.Identity?.Name;
            
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([FromBody] PostViewModel model)
        {   
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            int.TryParse(UserId, out int userIdAsInt);

            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    UserId = userIdAsInt,
                    Title = model.Title,
                    Content = model.Content, // Corrected from Description to Content
                    Location = model.Location,
                    Image = model.Image,
                    MaxPeople = model.MaxPeople, // Assuming MaxPeople is non-nullable
                    Date = model.Date, // Assuming Date is non-nullable
                    ExpireDate = model.ExpireDate // Assuming ExpireDate is non-nullable
                };

                await _postService.MakePost(post);

                return Json(new { success = true, message = "Post created successfully!" });
            }

            return Json(new { success = false, message = "Model state is not valid." });
        }
    }

}