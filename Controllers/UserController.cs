using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Models.Database; // Assuming this is where your User entity is defined
using SocialMedia.ViewModels; // Reference UserViewModel
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // [Authorize]
        public IActionResult Index()
        {
            var all_users = _userService.GetAllUsers();
            return View(all_users);
        }

        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                // User is already authenticated, redirect to "/Home"
                return RedirectToAction("Index", "Home");
            }

            // User user = new User();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            Console.WriteLine("Registering user");
            Console.WriteLine(model.Username);
            Console.WriteLine(model.Name);
            Console.WriteLine(model.Email);
            Console.WriteLine(model.Password);
            Console.WriteLine(model.ConfirmPassword);

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

                var result = await _userService.AddUser(user);
                if (result)
                {
                    return RedirectToAction("Login", "User");
                }
                // Handle false result if you chose to return a boolean from AddUser
            }
            catch (ArgumentException ex)
            {
                // Log the exception or set model state error
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                // User is already authenticated, redirect to "/Home"
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.AuthenticateUser(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            // Set the JWT token as a cookie
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict, // Adjust as needed
                Expires = DateTime.Now.AddDays(1) // Adjust expiration as needed
            });

            // You can return the token as part of a view model, set it in a cookie, or use it in another appropriate way depending on your application's needs
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool deleted = await _userService.DeleteUser(userId);
            if (deleted)
            {
                TempData["Message"] = "User deleted successfully.";
            }
            else
            {
                TempData["Error"] = "User not found.";
            }

            return RedirectToAction("Index");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("rk6IiDIGqZcwT+Vm+Qx6flz98AieqMhgzV73uGZ3mEN8Z6BSYhGywS9yviN7yL4K")); // Use a secure key from your configuration
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            };

            var token = new JwtSecurityToken(
                // issuer: "YourIssuer", // Optional
                // audience: "YourAudience", // Optional
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult SentEmail()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            // Get all cookies from the request
            var cookies = HttpContext.Request.Cookies;

            // Loop through the cookies and delete each one
            foreach (var cookie in cookies)
            {
                HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            return RedirectToAction("Login", "User");
        }
        
        [HttpPost]
         public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var result =   await _userService.UpdateUser(model, int.Parse(UserId));
            
            if (result)
            {
                return RedirectToAction("Post", "Profile");
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditImage ([FromBody] EditImageViewModel model)
        {
           if (!ModelState.IsValid)
            {
                return View(model);
            }

            var UserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var result =   await _userService.UpdateImage(model, int.Parse(UserId));
            
            if (result)
            {
                return RedirectToAction("Post", "Profile");
            }


            return View(model);
        }

    }
}
