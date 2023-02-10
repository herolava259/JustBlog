using FA.JustBlog.Service.CategoryService;
using FA.JustBlog.Service.PostService;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.UI.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("")]
        public IActionResult Index()
        {

            var d = _postService.GetAllPostView();
            if (d.Data == null)
            {
                return NotFound();
            }
            return View(d.Data);
        }
        [Route("{year}/{month}/{title}")]
        public IActionResult Details(int year, int month, string title)
        {
            var post = _postService.FindPost(year, month, title).Data;
            ViewBag.posts = post;
            return View();
        }

        [Route("post/{category}")]
        public IActionResult GetPostByCategory(string category)
        {
            var post = _postService.GetPostsByCategory(category).Data;
            return View(post);
        }

        [Route("{tag}")]
        public IActionResult GetPostByTag(string tag)
        {
            Console.WriteLine(tag);
            var post = _postService.GetPostsByTag(tag).Data;

            return View(post);
        }
    }
}
