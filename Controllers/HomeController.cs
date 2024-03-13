using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModels;
using System.Security.Claims;
using SocialMedia.Services;
using SocialMedia.Models.Database;
namespace SocialMedia.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PostService _postService;
    private readonly UserService _userService;

    private readonly CommentService _commentService;
    public HomeController(ILogger<HomeController> logger, PostService postService, UserService userService,CommentService commentService)
    {
        _logger = logger;
        _postService = postService;
        _userService = userService;
        _commentService = commentService;
    }

    // [HttpPost]
    public async Task<IActionResult> Index(string data)
    {
        var username = HttpContext.User.Identity?.Name;
        _logger.LogInformation($"Username from JWT: {username}");

        var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (UserId == null)
        {
            return RedirectToAction("Login", "User");
        }
        var user =  _userService.GetUserById(int.Parse(UserId));

        ViewData["Username"] = user.Name;
        ViewData["UserId"] = UserId;
        ViewData["UserImg"] = user.Image;

   


        var activity = new List<JoinActivity>();
        var userActivities =  _userService.GetUserActivities(int.Parse(UserId));
        activity.AddRange(userActivities.Take(3));

        // var posts = _postService.GetAllPosts();
        var posts = _postService.GetAllPosts();
        if (data != null)
        {
            posts = _postService.GetPostsByTitle(data);
        }
        var model = new HomeViewModel
        {
            Posts = posts,
            Activities = activity
        };
        if (data != null)
        {
            return View("JustPost", model);
        }
        return View(model); // Passes posts as a model to the view

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Post(int id)
    {
        var username = HttpContext.User.Identity?.Name;
        _logger.LogInformation($"Username from JWT: {username}");

        var UserIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(UserIdClaim, out var userId))
        {
            // Log error or handle parse failure
            return RedirectToAction("Login", "User");
        }

        var user = await _userService.GetUserByIdAsync(userId);
        ViewData["Username"] = user.Name;
        ViewData["UserId"] = UserIdClaim;
        ViewData["UserImg"] = user.Image;
        ViewData["PostId"] = id;

    var activity = new List<JoinActivity>();
    var userActivities = await _userService.GetUserActivitiesAsync(userId);
    activity.AddRange(userActivities.Take(3));
    var post = _postService.GetPostByPostId(id);
    var posts = await _postService.GetAllPostsAsync();
    var comment = _commentService.GetCommentsByPostId(id);
    if (post == null)
    {
        return RedirectToAction("Index");
    }

    var model = new HomeViewModel
    {
        Posts = posts,
        Activities = activity,
        Post = post,
        Comments = comment
    };


        return View(model);

    }

}
