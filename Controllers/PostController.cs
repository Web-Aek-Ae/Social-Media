using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Namespace where Post is located
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly PostService _postService;

        private readonly CommentService _commentService;
        private readonly GroupService _groupService;
        private readonly ILogger<PostController> _logger;

        private readonly UserService _userService;
        public PostController(PostService postService, ILogger<PostController> logger, CategoryService categoryService, GroupService groupService, CommentService commentService, UserService userService)
        {
            _postService = postService;
            _logger = logger;
            _categoryService = categoryService;
            _groupService = groupService;
            _commentService = commentService;

            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? id)
        {


            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _userService.GetUserById(int.Parse(UserId));


            ViewData["UserId"] = UserId;
            ViewData["Username"] = user.Name;
            ViewData["UserImage"] = user.Image;

            var activity = new List<JoinActivity>();
            var userActivities = _userService.GetUserActivities(int.Parse(UserId));
            activity.AddRange(userActivities.Take(3));

            var model = new PostViewModel
            {
                Title = "", // Add the missing Title property
                Content = "", // Add the missing Content property
                Categories = _categoryService.GetAllCategories().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList(),
                Date = DateTime.Now,
                ExpireDate = DateTime.Now,
                Time = DateTime.Now.ToString("HH:mm"),
                MaxPeople = 1,
                Location = "Bangkok",
                Group = _groupService.GetGroupById(id),
                Activities = activity
            };
            if (model.Group != null)
            {

                model.GroupId = id;
            }

            if (model.Categories.Count == 0)
            {
                throw new Exception("No categories found.");
            }

            // var username = HttpContext.User.Identity?.Name;


            return View(model);
        }

        // [HttpPost]
        // public async Task<IActionResult> Edit([FromBody] PostViewModel model, int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }

        //     var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //     if (UserId == null)
        //     {
        //         return RedirectToAction("Login", "User");
        //     }

        //     var result = await _postService.UpdatePost(model,id);

        //     if (result)
        //     {
        //         return RedirectToAction("Post", "Profile");
        //     }


        //     return View(model);
        // }
        // [HttpGet]
        // public IActionResult Edit(int id)
        // {
        //     ViewBag.postId = id;
        //     return View();
        // }


        // [HttpPost("CreatePost")]
        // [Authorize]
        // public async Task<ActionResult> CreatePost([FromBody] PostViewModel model)
        // {

        //     if (model == null)
        //     {
        //         return BadRequest("Model cannot be null.");
        //     }
        //     _logger.LogInformation("Creating post with title: {Title}", model.Title);
        //     var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //     // DateTime.TryParseExact(model.Time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime);
        //     if (!int.TryParse(UserId, out int userIdAsInt))
        //     {
        //         return Json(new { success = false, message = "User ID is invalid." });
        //     }
        //     if (!DateTime.TryParse("2000-01-01 " + model.Time, out DateTime fullDateTime))
        //     {
        //         return Json(new { success = false, message = "Invalid time format." });
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             var post = new Post
        //             {
        //                 UserId = userIdAsInt,
        //                 GroupId = model.GroupId,
        //                 Title = model.Title,
        //                 Content = model.Content,
        //                 Location = model.Location,
        //                 CategoryId = model.SelectedCategoryId,
        //                 Time = fullDateTime,
        //                 Image = model.Image,
        //                 MaxPeople = model.MaxPeople,
        //                 Date = model.Date,
        //                 ExpireDate = model.ExpireDate
        //             };
        //             post.Time = DateTime.SpecifyKind(fullDateTime, DateTimeKind.Utc);
        //             post.Date = DateTime.SpecifyKind(model.Date, DateTimeKind.Utc);
        //             post.ExpireDate = DateTime.SpecifyKind(model.ExpireDate, DateTimeKind.Utc);

        //             // If you know a DateTime value is in local time and needs to be converted to UTC
        //             post.Date = model.Date.ToUniversalTime();
        //             post.ExpireDate = model.ExpireDate.ToUniversalTime();


        //             await _postService.MakePost(post);

        //             return Json(new { success = true, message = "Post created successfully!" });
        //         }
        //         catch (Exception ex)
        //         {
        //             _logger.LogError(ex, "Error creating post.");

        //             // Return a generic error message
        //             return Json(new { success = false, message = "An error occurred while creating the post." });
        //         }
        //     }
        //     else
        //     {
        //         // Collect and return model state errors
        //         var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        //         var errorMessage = string.Join(" ", errors);

        //         return Json(new { success = false, message = errorMessage });
        //     }
        // }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateComment([FromBody] CommentViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null.");
            }
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var comment = new Comment
                    {
                        Content = model.Content,
                        UserId = userIdAsInt,
                        PostId = model.PostId,
                    };

                    await _commentService.AddComment(comment);
                    return Json(new { success = true, message = "Comment created successfully!" });
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