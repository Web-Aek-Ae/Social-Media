using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Security.Claims;
using SocialMedia.Services;
namespace SocialMedia.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PostService _postService;
    public HomeController(ILogger<HomeController> logger , PostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    public IActionResult Index()
    {
        var username = HttpContext.User.Identity?.Name;
        _logger.LogInformation($"Username from JWT: {username}");

        
        ViewData["Username"] = username;
        var posts = _postService.GetAllPosts();
        return View(posts); // Passes posts as a model to the view
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
}
