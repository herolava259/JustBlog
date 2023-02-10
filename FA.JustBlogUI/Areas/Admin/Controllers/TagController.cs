using FA.JustBlog.Common;
using FA.JustBlog.Model.Tag;
using FA.JustBlog.Service.TagService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Tag")]
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("Index")]
       [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Index()
        {
            var d = _tagService.GetAllTag().Data.ToList();
            return View(d);
        }

        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d = _tagService.FindById(id).Data;

            if (d == null)
            {
                return NotFound();
            }

            return View(d);
        }

        [Route("Create")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,UrlSlug,Count")] TagModel tag)
        {
            if (ModelState.IsValid)
            {
                _tagService.CreateTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }


        [Route("Edit/{id}")]
        //[Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.FindById(id).Data;
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }


        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,UrlSlug,Count")] TagModel tag)
        {

            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tagService.UpdateTag(tag);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TagExists(tag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    Console.WriteLine(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        private bool TagExists(int id)
        {
            var d = _tagService.FindById(id).Data;
            if (d == null)
            {
                return false;
            }
            return true;
        }


        [Route("Delete/{id}")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.FindById(id).Data;
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Admin/Categories/Delete/5
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [Bind("Id,Name,Description,UrlSlug,Count")] TagModel tag)
        {
            _tagService.DeleteTag(tag);
            return RedirectToAction(nameof(Index));
        }
    }
}
