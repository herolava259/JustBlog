using FA.JustBlog.Common;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Route("Index")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.Users.ToListAsync();
            return View(user);
        }

        [Route("Details")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(id);

            ViewBag.RoleNames = await userManager.GetRolesAsync(user);

            return View(user);
        }

        [Route("Create")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Age = model.Age,
                    Address = model.Address
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var results = await userManager.AddToRolesAsync(user, selectedRoles);
                        if (!results.Succeeded)
                        {
                            ModelState.AddModelError("", "Failed");
                            ViewBag.Roles = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error");
                    ViewBag.Roles = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
            return View(model);
        }



        [Route("Edit")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var roles = await roleManager.Roles.ToListAsync();
            var roleItems = roles.Select(role =>
                 new SelectListItem(
                     role.Name,
                     role.Id.ToString(),
                     userRoles.Any(ur => ur.Contains(role.Name)))).ToList();
            var result = new UserModel()
            {
                Address = user.Address,
                Age = user.Age,
                FullName = user.FullName,
                Email = user.Email,
                Id = user.Id.ToString(),
                RolesList = roleItems
            };

            return View(result);
        }


        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }
                var userRoles = await userManager.GetRolesAsync(user);

                var rolesToAdd = new List<string>();
                var rolesToDelete = new List<string>();
                foreach (var role in model.RolesList)
                {
                    var assignedInDb = userRoles.FirstOrDefault(ur => ur == role.Text);
                    if (role.Selected)
                    {
                        if (assignedInDb == null)
                        {
                            rolesToAdd.Add(role.Text);
                        }
                    }
                    else
                    {
                        if (assignedInDb != null)
                        {
                            rolesToDelete.Add(role.Text);
                        }
                    }
                }
                if (rolesToAdd.Any())
                {
                    await userManager.AddToRolesAsync(user, rolesToAdd);
                }
                if (rolesToDelete.Any())
                {
                    await userManager.RemoveFromRolesAsync(user, rolesToDelete);
                }
                user.Address = model.Address;
                user.Age = model.Age;
                user.FullName = model.FullName;
                user.UserName = model.Email;
                user.Email = model.Email;
                userManager.UpdateAsync(user);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Edit failed.");
            return View(model);
        }

        [Route("Delete")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userModel = new UserModel();
            userModel.Email = user.Email;
            userModel.Address = user.Address;
            userModel.Age = user.Age;
            userModel.FullName = user.FullName;
            userModel.Id = user.Id;
            return View(userModel);
        }

        [Route("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserModel model)
        {

            var user = await userManager.FindByIdAsync(model.Id);
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Delete Failed");
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
