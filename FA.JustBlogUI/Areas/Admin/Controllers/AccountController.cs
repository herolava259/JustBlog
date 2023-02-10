using FA.JustBlog.Common;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,
                                      SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _signInManager.UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
            var respon = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (respon.Succeeded)
            {
                var claim = new List<Claim>
                {
                    new Claim("name" , "pass")
                };
                var roles = await _signInManager.UserManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    var roleClaim = string.Join(",", roles);
                    claim.Add(new Claim("Roles", roleClaim));
                }
                await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claim);

                var list = await _userManager.GetRolesAsync(user);
                var data = list.Any(t => t.Equals(SD.Role_Blog_Owner) || t.Equals(SD.Role_Contributor));
                if (data)
                {

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                var data1 = list.Any(t => t.Equals(SD.Role_User));
                if (data1)
                {
                    return RedirectToAction("Index", "Post", new { area = "" });
                }
                return RedirectToAction("Register", "Account");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View(model);
        }


        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Age= model.Age,
                    Address = model.Address
                };
                Console.WriteLine(user.ToString());
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }



        [Route("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return ();
        }
    }
}
