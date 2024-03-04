using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Assuming this is where your User entity is defined
using SocialMedia.ViewModels; // Reference UserViewModel
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


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

            return View();
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
                return Ok("Kuay");
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


    }


}