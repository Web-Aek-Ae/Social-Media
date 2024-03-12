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

        private readonly PostService _postService;

        private readonly GroupmemberService _groupmemberService;

        public GroupController(GroupService groupService ,GroupmemberService groupmemberService, PostService postService)
        {
            _groupService = groupService;
            _groupmemberService = groupmemberService;
            _postService = postService;

        }
        public IActionResult Index()
        {
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var specificClaimUsername = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Use the username for your application logic...
            ViewData["Username"] = username;


            var groupspost = _groupService.GetAllGroups()

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
            var groupspost = _groupService.GetAllGroups();
            return View(groupspost);
        }
        
        public IActionResult Details(int id){
            var username = HttpContext.User.Identity?.Name;
            // Alternatively, if the username is stored in a specific claim type
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Use the username for your application logic...
            ViewData["UserId"] = UserId;
            ViewData["Username"] = username;
            var posts = _postService.GetPostsByGroupId(id);
            var group = _groupService.GetGroupById(id);
            var detailsmodel = new DetailsModels{
                Posts = posts,
                Group = group
            };
           
            return View(detailsmodel);
        }

        
        [HttpPost]
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
            return Ok(model.Groupname);
        }

        
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> JoinGroup(int id)
        {
            // int groupid = Int32.Parse(id);
            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // var existingMembership = await _groupmemberService.GetGroupMembershipAsync(userId, id);
            if (!int.TryParse(UserId, out int userIdAsInt))
            {
                return Json(new { success = false, message = "User ID is invalid." });
            }
            var groupmember = new GroupMember{
                UserId = userIdAsInt,
                GroupId = id,
            };
            var result = await _groupmemberService.JoinGroup(groupmember);
            if(result){
                return Json(new { success = true, message = "Join group successfully!" });
            }
            else{
                return Json(new { success = true, message = "Error to join group" });
            }
            
        }
    }


}