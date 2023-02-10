using FA.JustBlog.Common;
using FA.JustBlog.Model.Category;
using FA.JustBlog.Service.CategoryService;
using FA.JustBlog.Service.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("category")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Index()
        {
            var id = _categoryService.GetCategory().Data.ToList();
            return View("Index", id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,UrlSlug,Picture")] CreateCategoryModel category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.FindById(id).Data;
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,UrlSlug")] UpdateCategoryModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var c = _categoryService.UpdateCategory(category);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        private bool CategoryExists(int id)
        {
            var d = _categoryService.FindById(id).Data;
            if (d == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.FindById(id).Data;
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [Bind("Id,Name,Description,UrlSlug")] UpdateCategoryModel category)
        {
            _categoryService.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
