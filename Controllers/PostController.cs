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

        private readonly JoinActivityService _joinActivityService;

        private readonly UserService _userService;
        public PostController(PostService postService, ILogger<PostController> logger, CategoryService categoryService, GroupService groupService, CommentService commentService, UserService userService , JoinActivityService joinActivityService)
        {
            _postService = postService;
            _logger = logger;
            _categoryService = categoryService;
            _groupService = groupService;
            _commentService = commentService;
            _joinActivityService = joinActivityService;

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

            var user = _userService.GetUserById(userIdAsInt);

            if (ModelState.IsValid)
            {
                try
                {
                    var post = new Post
                    {
                        UserId = userIdAsInt,
                        GroupId = model.GroupId,
                        Title = model.Title,
                        Content = model.Content,
                        Location = model.Location,
                        CategoryId = model.SelectedCategoryId,
                        Time = fullDateTime,
                        Image = model.Image,
                        MaxPeople = model.MaxPeople,
                        Date = model.Date,
                        ExpireDate = model.ExpireDate,
                        CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
                    };
                    post.Time = DateTime.SpecifyKind(fullDateTime, DateTimeKind.Utc);

                    // If you know a DateTime value is in local time and needs to be converted to UTC
                    post.Date = model.Date.AddHours(7).ToUniversalTime();
                    post.ExpireDate = model.ExpireDate.AddHours(7).ToUniversalTime();
                    post.CreatedAt = post.CreatedAt.ToUniversalTime();


                    
                    var new_post =  await _postService.MakePost(post);
                    
                    await _joinActivityService.ToggleActivity(new_post.PostId , UserId);

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

        [HttpPost]
        public async Task<ActionResult> DeletePost([FromBody] DeletePostViewModel model)
        {
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }




            var post = _postService.GetPostByPostId(model.PostId);
            if (post == null)
            {
                return Json(new { success = false, message = "Post not found." });
            }
            if (post.UserId != userIdAsInt)
            {
                return Json(new { success = false, message = "User does not have permission to delete this post." });
            }
            await _postService.DeletePost(model.PostId);
            return Json(new { success = true, message = "Post deleted successfully!" });
        }

        [HttpPost]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeStatusViewModel model)
        {
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }

            var post = _postService.GetPostByPostId(model.PostId);
            if (post == null)
            {
                return Json(new { success = false, message = "Post not found." });
            }
            if (post.UserId != userIdAsInt)
            {
                return Json(new { success = false, message = "User does not have permission to change the status of this post." });
            }
            post.PostStatus = model.PostStatus;
            await _postService.UpdatePost(post);
            return Json(new { success = true, message = "Post status changed successfully!" });
        }

    }
}