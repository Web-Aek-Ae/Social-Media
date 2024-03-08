using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Namespace where Post is located
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;


namespace SocialMedia.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }
        public IActionResult Index()
        {
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var specificClaimUsername = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Use the username for your application logic...
            ViewData["Username"] = username;

            var groupspost = _groupService.GetAllGroups();

            return View(groupspost);
        }

        public IActionResult Create(){
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Use the username for your application logic...
            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View();
        }

        public IActionResult Recommend(){
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Use the username for your application logic...
            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View();
        }
        
        public IActionResult Details(){
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Use the username for your application logic...
            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View();
        }


        
        [HttpPost("CreateGroup")]
        [Authorize]
        public async Task<ActionResult> CreateGroup([FromBody] GroupViewModel model)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest("model invalid");
            }
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }
            try{
                var group = new Group
                {   
                    UserId = userIdAsInt,
                    Name = model.Groupname,
                    Description = model.Description,
                    Image = model.Image,

                };
                var result = await _groupService.AddGroup(group);
                if (result)
                {
                    return Json(new { success = true, message = "Post created successfully!" });
                }

            }
            catch (ArgumentException ex)
            {
                // Log the exception or set model state error
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            Console.WriteLine(model.Groupname);
            return Ok(model.Groupname);
        }
        // public IActionResult CreateGroup()
        // {
        //     var username = HttpContext.User.Identity?.Name;

        //     var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //     ViewData["UserId"] = UserId;
        //     ViewData["Username"] = username;
        //     return View();
        // }

        // [HttpPost]
        // public Task<ActionResult> JoinGroup([FromBody] int GroupId)
        // {
        //     var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //     if (!int.TryParse(UserId, out int userIdAsInt))
        //     {
        //         return Json(new { success = false, message = "User ID is invalid." });
        //     }
        //     var groupmember = new GroupMember
        //     {
        //         UserId = userIdAsInt,
        //         GroupId = GroupId,

        //     };
        
           

        // }

    }


}