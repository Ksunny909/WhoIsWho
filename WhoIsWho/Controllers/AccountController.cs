using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WhoIsWho.Models.Entities;
using WhoIsWho.Models;
using System.Security.Claims;

namespace WhoIsWho.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return View();
            }

            if(! await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return View();
            }
            var claims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            roles.ToList().ForEach(r => claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));
            claims.Add(new Claim("Id", user.Id.ToString()));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principals = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principals);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var admin = await _userManager.FindByEmailAsync(model.Email);
            if (admin == null)
            {
                admin = new ApplicationUser { Email = model.Email, UserName = model.Email };
                await _userManager.CreateAsync(admin, model.Password);
            }

            return RedirectToAction("Login");
        }
    }
}
