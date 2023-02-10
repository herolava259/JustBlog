using FA.JustBlog.Common;
using FA.JustBlog.Model.Post;
using FA.JustBlog.Service.CategoryService;
using FA.JustBlog.Service.PostService;
using FA.JustBlog.Service.TagService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FA.JustBlog.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Post")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postService"></param>
        /// <param name="categoryService"></param>
        /// <param name="tagService"></param>
        public PostController(IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Index()
        {
            var d = _postService.GetPost().Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Details")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d = _postService.GetPostById(id).Data;

            var t = _categoryService.GetCategory().Data.ToList();
            foreach (var cate in t)
            {
                if (cate.Id == d.CategoryId)
                {
                    ViewBag.catepost = cate.Name;
                }
            }

            var tag = _tagService.GetTagMapByPostId(id).Data.ToList();
            string ta = "";
            Console.WriteLine(tag.Count);
            for (int i = 0; i < tag.Count; i++)
            {
                ta += $"{tag[i].Name},";
            }
            ViewBag.posttag = ta;
            if (d == null)
            {
                return NotFound();
            }

            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        //[Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategory().Data, "CateId", "Name");
            ViewBag.Tags = _tagService.GetAllTag().Data;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="selectedTagModels"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,ShortDescription,PostContent,UrlSlug,CategoryId,Published,PostedOn")] PostModel post, IEnumerable<int> selectedTagModels)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(post, selectedTagModels);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tags = _tagService.GetAllTag().Data;
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategory().Data, "CateId", "Name");
            return View(post);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        //[Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _postService.GetPostById(id).Data;
            if (tag == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategory().Data, "CateId", "Name");
            ViewBag.Tags = _tagService.GetAllTag().Data;
            return View(tag);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <param name="selectedTagModels"></param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,ShortDescription,PostContent,UrlSlug,CategoryId,Published,PostedOn")] PostModel post, IEnumerable<int> selectedTagModels)
        {

            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _postService.UpdatePost(post, selectedTagModels);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!PostExists(post.Id))
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
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategory().Data, "CateId", "Name");
            ViewBag.Tags = _tagService.GetAllTag().Data;

            return View(post);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PostExists(int id)
        {
            var d = _postService.GetPostById(id).Data;
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

            var d = _postService.GetPostById(id).Data;

            var t = _categoryService.GetCategory().Data.ToList();
            foreach (var cate in t)
            {
                if (cate.Id == d.CategoryId)
                {
                    ViewBag.catepost = cate.Name;
                }
            }

            var tag = _tagService.GetTagMapByPostId(id).Data.ToList();
            string ta = "";
            Console.WriteLine(tag.Count);
            for (int i = 0; i < tag.Count; i++)
            {
                ta += $"{tag[i].Name},";
            }
            ViewBag.posttag = ta;
            if (d == null)
            {
                return NotFound();
            }

            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [Bind("Id,Title,ShortDescription,PostContent,UrlSlug,CategoryId,Published,PostedOn")] PostModel post)
        {
            _postService.DeletePost(post);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetLatestPost")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult GetLatestPost()
        {
            var d = _postService.GetLatestPost(8).Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetMostViewedPost")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult GetMostViewedPost()
        {
            var d = _postService.GetMostViewedPost(8).Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetPublishedPost")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public IActionResult GetPublishedPost()
        {
            var d = _postService.GetPublishedPost().Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetUnPublishedPost")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public IActionResult GetUnPublishedPost()
        {
            var d = _postService.GetUnPublishedPost().Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetMostInterestingPosts")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult GetMostInterestingPosts()
        {
            var d = _postService.GetPost().Data.ToList();
            return View(d);
        }
    }
}
