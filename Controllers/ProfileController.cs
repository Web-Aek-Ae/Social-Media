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
        public async Task<IActionResult> EditProfile(int userId, [Bind("UserId,Name,Username,Email,Password")] User userProfile)
        {
        if (userId != userProfile.UserId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            
            try
            {
                await _userService.UpdateUser(userProfile);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            //return RedirectToAction(nameof(Index));
        }
        return View(userProfile);
    }
    } 
    
}
