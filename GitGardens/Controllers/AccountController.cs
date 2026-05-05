using GitGardens.Data;
using GitGardens.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GitGardens.Controllers
{

    /*
       Title: ASP.NET Core MVC CRUD - NET 6 MVC CRUD  
       Operations Using Entity Framework Core and SQL Server  
       Author: Sameer Saini  
       Date: 2 years ago
       Date accessed: 05/03/2026
       Code version : 1  
       Availability: https://youtu.be/2Cp8Ti_f9Gk?feature=shared
    */

    /*
       Title: ASP.NET 8 MVC Tutorial for Beginners - C# web development made easy  
       Operations Using Entity Framework Core and SQL Server  
       Author: tutorialsEU  
       Date: 1 years ago
       Date accessed: 05/03/2026
       Code version : 1  
       Availability: https://youtu.be/xuFdrXqpPB0?feature=shared
    */

    /*
       Title: Asp.Net Core MVC & Identity UI - User Registration and Login
       Author: CodAffection  
       Date: 2 years ago
       Date accessed: 05/03/2026
       Code version : 1  
       Availability: https://youtu.be/wzaoQiS_9dI?feature=shared
    */


    public class AccountController : Controller
    {

        private readonly GitGardensDBContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        // DbContext Constructor
        public AccountController(GitGardensDBContext Context, IPasswordHasher<User> PasswordHasher)
        {
            context = Context;
            passwordHasher = PasswordHasher;
        }

        //////////////////////////////////////////////////////////Register//////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                // Check if Email is already taken
                if (context.Users.Any(u => u.Email == model.Email))
                {
                    TempData["Email"] = "Email Already in Use";
                    return View(model);
                }

                // Create User
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = passwordHasher.HashPassword(null, model.Password),  // Hash Password
                    RoleID = model.RoleID
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();   // Save Changes

                // Redirect to Login After Successful Registeration
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        //////////////////////////////////////////////////////////User Profile//////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);              // Retrieve the Logged in user email

            if (email == null)
            {
                return NotFound();
            }

            var user = await context.Users.Include(u => u.Role).Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //////////////////////////////////////////////////////////Login + Logout//////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            // Check Model State
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = context.Users.Include(u => u.Role)
                .FirstOrDefault(u => u.Email == model.Email);

            // Validate Credentials
            if (user == null || !passwordHasher.VerifyHashedPassword(user, user.Password, model.Password).Equals(PasswordVerificationResult.Success))
            {
                TempData["Fail"] = "Invalid Email or Password";
                return View(model);
            }

            // Create user claims - Pieces of information about this user - Creates login session for eah respective user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),   // User Unique Identitfier
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)  // User role for Role based Access control
            };

            // Caims Identity Object representing autheticated user identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign user in by creating an Authenticatoon Cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("CreateGarden", "Garden");

            // Redirect Users Based of Role
            switch (user.Role.RoleName)
            {
                case "Admin":
                    return RedirectToAction("Dashboard", "Admin");
                case "User":
                    return RedirectToAction("Dashboard", "User");
                default:
                    return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        //////////////////////////////////////////////////////////Edit Profile//////////////////////////////////////////////////////////

        /*
        Title: Disclosure of AI Usage in my Assessment.
        • Section: EditProfile & DeleteProfile.
        • AI Tool: Gemini
        • Purpose/intention : Design and framework of EditProfile & DeleteProfile functionalities.
        • Date(s) 05/05/2026.
        • https://gemini.google.com/share/75f6e6bedd8e
        */

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return NotFound();

            // Map the database User to our Edit ViewModel
            var model = new EditProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return NotFound();

            
            user.FullName = model.FullName;

            // Only update password if the user typed something in that box
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                user.Password = passwordHasher.HashPassword(user, model.NewPassword);
            }

            context.Users.Update(user);
            await context.SaveChangesAsync();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        //////////////////////////////////////////////////////////Delete Profile//////////////////////////////////////////////////////////

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return NotFound();

            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfileConfirmed()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
