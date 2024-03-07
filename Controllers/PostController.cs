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
    public class PostController : Controller
    {   
        private readonly CategoryService _categoryService;
        private readonly PostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(PostService postService, ILogger<PostController> logger , CategoryService categoryService)
        {
            _postService = postService;
            _logger = logger;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new PostViewModel
            {
                Title = "", // Add the missing Title property
                Content = "", // Add the missing Content property
                Categories = _categoryService.GetAllCategories().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList()
            };

            // Optionally add a default choice
            model.Categories.Insert(0, new SelectListItem { Text = "Select a category", Value = "" });

            var username = HttpContext.User.Identity?.Name;

            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View(model);
        }

        [HttpPost("CreatePost")]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] PostViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null.");
            }
            _logger.LogInformation("Creating post with title: {Title}", model.Title);
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // DateTime.TryParseExact(model.Time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime);
            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }
            if (!DateTime.TryParse("2000-01-01 " + model.Time, out DateTime fullDateTime))
            {
                return Json(new { success = false, message = "Invalid time format." });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var post = new Post
                    {
                        UserId = userIdAsInt,
                        Title = model.Title,
                        Content = model.Content,
                        Location = model.Location,
                        Time = fullDateTime,
                        Image = model.Image,
                        MaxPeople = model.MaxPeople,
                        Date = model.Date,
                        ExpireDate = model.ExpireDate
                    };
                    post.Time = DateTime.SpecifyKind(fullDateTime, DateTimeKind.Utc);
                    post.Date = DateTime.SpecifyKind(model.Date, DateTimeKind.Utc);
                    post.ExpireDate = DateTime.SpecifyKind(model.ExpireDate, DateTimeKind.Utc);

                    // If you know a DateTime value is in local time and needs to be converted to UTC
                    post.Date = model.Date.ToUniversalTime();
                    post.ExpireDate = model.ExpireDate.ToUniversalTime();


                    await _postService.MakePost(post);

                    return Json(new { success = true, message = "Post created successfully!" });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating post.");

                    // Return a generic error message
                    return Json(new { success = false, message = "An error occurred while creating the post." });
                }
            }
            else
            {
                // Collect and return model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                var errorMessage = string.Join(" ", errors);

                return Json(new { success = false, message = errorMessage });
            }
        }


    }
}