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
    public HomeController(ILogger<HomeController> logger, PostService postService, UserService userService)
    {
        _logger = logger;
        _postService = postService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        var username = HttpContext.User.Identity?.Name;
        _logger.LogInformation($"Username from JWT: {username}");

        var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (UserId == null)
        {
            return RedirectToAction("Login", "User");
        }
        var user =  _userService.GetUserById(int.Parse(UserId));

        ViewData["Username"] = username;
        ViewData["UserId"] = UserId;
        ViewData["UserImg"] = user.Image;

   


        var activity = new List<JoinActivity>();
        var userActivities =  _userService.GetUserActivities(int.Parse(UserId));
        activity.AddRange(userActivities.Take(3));

        var posts = _postService.GetAllPosts();
        var model = new HomeViewModel
        {
            Posts = posts,
            Activities = activity
        };

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
        ViewData["Username"] = username;
        ViewData["UserId"] = UserIdClaim;

        if (!int.TryParse(UserIdClaim, out var userId))
        {
            // Log error or handle parse failure
            return RedirectToAction("Login", "User");
        }

        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        var activity = new List<JoinActivity>();
        var userActivities = await _userService.GetUserActivitiesAsync(userId);
        activity.AddRange(userActivities.Take(3));
        var post = _postService.GetPostByPostId(id);
        var posts = await _postService.GetAllPostsAsync();
        if (post == null)
        {
            return RedirectToAction("Index");
        }

        var model = new HomeViewModel
        {
            Posts = posts,
            Activities = activity,
            Post = post
        };

        return View(model);

    }

}
