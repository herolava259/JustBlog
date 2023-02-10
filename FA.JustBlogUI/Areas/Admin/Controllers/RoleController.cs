using FA.JustBlog.Common;
using FA.JustBlog.Model.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("role")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [Route("Index")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public async Task<IActionResult> Index()
        {
            return View(roleManager.Roles);
        }

        [Route("Create")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.Name
                };
                var result = await roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
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
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleModel = new RoleModel();
            roleModel.Name = role.Name;
            roleModel.Id = role.Id;
            return View(roleModel);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Id")] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                await roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
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
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleModel = new RoleModel();
            roleModel.Name = role.Name;
            roleModel.Id = role.Id;
            return View(roleModel);
        }

        [Route("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Name,Id")] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                await roleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
