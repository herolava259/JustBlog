using FA.JustBlog.Common;
using FA.JustBlog.Model.Comment;
using FA.JustBlog.Service.CommentService;
using FA.JustBlog.Service.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Comment")]
    //[Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentService"></param>
        /// <param name="postService"></param>
        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Index()
        {
            var d = _commentService.GetAllComment().Data.ToList();
            return View(d);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d = _commentService.FindById(id).Data;

            var t = _postService.GetAllPostView().Data.ToList();
            foreach (var post in t)
            {
                if (post.Id==d.PostId)
                {
                    ViewBag.commentpost = post.Title;
                }
            }


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
        [Authorize(Roles = $"{SD.Role_Blog_Owner}")]
        public IActionResult Create()
        {
            ViewBag.Comment = new SelectList(_postService.GetPost().Data, "Id", "Title");
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,CommentHeader,CommentText,PostId,CommentTime")] CommentModel comment)
        {
            if (ModelState.IsValid)
            {
                var t = _commentService.CreateComment(comment);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            ViewBag.Comment = new SelectList(_postService.GetPost().Data, "Id", "Title");
            return View(comment);
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

            var tag = _commentService.FindById(id).Data;
            if (tag == null)
            {
                return NotFound();
            }
            ViewBag.Comment = new SelectList(_postService.GetPost().Data, "Id", "Title");
            return View(tag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,CommentHeader,CommentText,PostId,CommentTime")] CommentModel comment)
        {

            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _commentService.UpdateComment(comment);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TagExists(comment.Id))
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
            ViewBag.Comment = new SelectList(_postService.GetPost().Data, "Id", "Title");

            return View(comment);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TagExists(int id)
        {
            var d = _commentService.FindById(id).Data;
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

            var tag = _commentService.FindById(id).Data;

            var t = _postService.GetAllPostView().Data.ToList();
            foreach (var post in t)
            {
                if (post.Id == tag.PostId)
                {
                    ViewBag.commentpost = post.Title;
                }
            }

            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [Bind("Id,Name,Email,CommentHeader,CommentText,PostId,CommentTime")] CommentModel comment)
        {
            _commentService.DeleteComment(comment);
            return RedirectToAction(nameof(Index));
        }
    }
}
