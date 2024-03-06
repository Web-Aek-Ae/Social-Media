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

        private readonly GroupmemberService _groupmemberService;

        public GroupController(GroupService groupService,GroupmemberService groupmemberService)
        {
            _groupService = groupService;
            _groupmemberService = groupmemberService;
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
        public IActionResult Postpage()
        {
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var specificClaimUsername = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Use the username for your application logic...
            ViewData["Username"] = username;

            return View();
        }
        

        [HttpPost]
        public async Task<ActionResult> Create(GroupViewModel model)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest("model invalid");
            }
            try{
                var group = new Group
                {
                    Name = model.Groupname,
                    Description = model.Description,

                };
                var result = await _groupService.AddGroup(group);
                if (result)
                {
                    return RedirectToAction("Index", "Group");
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
        public IActionResult CreateGroup()
        {
            var username = HttpContext.User.Identity?.Name;

            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            return View();
        }

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