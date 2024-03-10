using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialMedia.Services;
using SocialMedia.ViewModels; // Namespace where TableViewModel is located
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using SocialMedia.Models.Database;

namespace SocialMedia.Controllers
{

    public class ProfileController : Controller
    {
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            var username = HttpContext.User.Identity?.Name;
            var name = HttpContext.User.Identity?.Name;
            ViewData["Username"] = username;
            ViewData["Name"] = name;
        
            return View();
        }

        public IActionResult Joined()
        {
            return View();
        }

        public IActionResult Likes()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();

        }
       
        // GET: Profile/EditProfile
        public async Task<IActionResult> EditProfile(int userId)
        {
            // ดึงข้อมูลโปรไฟล์ของผู้ใช้จากฐานข้อมูล
            var userProfile = await _userService.GetUserById(userId);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: Profile/EditProfile
        [HttpPost]
        public async Task<ActionResult> EditProfile(ProfileViewModel model)
        {
            Console.WriteLine("Edit Profile");
            Console.WriteLine(model.Username);
            Console.WriteLine(model.Name);
            Console.WriteLine(model.Email);
            Console.WriteLine(model.Password);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = new User
                {
                    Username = model.Username,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password // This should be hashed inside AddUser
                };

                var result = await _userService.UpdateUser(user);
            }
            catch (ArgumentException ex)
            {
                // Log the exception or set model state error
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }
    } 
    
}
