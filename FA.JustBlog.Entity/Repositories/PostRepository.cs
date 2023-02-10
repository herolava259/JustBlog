using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Entity.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Entity.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly JustBlogContext _context;

        public PostRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// CountPostsForCategory 
        /// </summary>
        /// <param name="category"></param>
        /// <returns>int</returns>
        public int CountPostsForCategory(string category)
        {
            var query = from p in _context.Posts
                        join c in _context.Categories
                        on p.CategoryId equals c.Id
                        where c.Name == category
                        select p;
            return query.ToList().Count();
        }
        /// <summary>
        /// CountPostsForTag
        /// <param name="tag"></param>
        /// <returns>int</returns>
        public int CountPostsForTag(string tag)
        {
            var query = from p in _context.Posts
                        join pt in _context.PostTagMaps
                        on p.Id equals pt.PostId
                        join t in _context.Tags
                        on pt.TagId equals t.Id
                        where t.Name == tag
                        select p;

            return query.ToList().Count();
        }
        /// <summary>
        /// FindPost By year, month, urlSlug
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="urlSlug"></param>
        /// <returns>Post</returns>
        public Post FindPost(int year, int month, string urlSlug)
        {
            return _context.Posts.Where(x => x.PostedOn.Year == year && x.PostedOn.Month == month && x.UrlSlug == urlSlug).FirstOrDefault();
        }


        /// <summary>
        /// GetLatestPost
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetLatestPost(int size)
        {
            return _context.Posts.OrderByDescending(x => x.PostedOn).Take(size).ToList();
        }


        /// <summary>
        /// GetPostsByCategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetPostsByCategory(string category)
        {
            var query = (from p in _context.Posts
                         join c in _context.Categories
                         on p.CategoryId equals c.Id
                         where c.Name == category
                         select p).Include(x => x.PostTagMaps)
                    .ThenInclude(x => x.Tags)
                    .Include(x => x.Category);
            return query.ToList();
        }
        /// <summary>
        /// GetPostsByMonth
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetPostsByMonth(DateTime monthYear)
        {
            return _context.Posts.Where(x => x.PostedOn.Month == monthYear.Month).ToList();
        }
        /// <summary>
        /// GetPostsByTag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetPostsByTag(string tag)
        {
            var query = (from p in _context.Posts
                         join pt in _context.PostTagMaps
                         on p.Id equals pt.PostId
                         join t in _context.Tags
                         on pt.TagId equals t.Id
                         where t.UrlSlug.ToLower() == tag.ToLower()
                         select p).Include(x => x.PostTagMaps)
                    .ThenInclude(x => x.Tags)
                    .Include(x => x.Category);
            return query.ToList();
        }
        /// <summary>
        /// GetPublisedPosts
        /// </summary>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetPublisedPosts()
        {
            return _context.Posts.Where(x => x.Published==true).ToList();
        }
        /// <summary>
        /// GetUnpublisedPosts
        /// </summary>
        /// <returns>Post</returns>
        public IEnumerable<Post> GetUnpublisedPosts()
        {
            return _context.Posts.Where(x => x.Published==false).ToList();
        }
        /// <summary>
        /// GetMostViewedPost
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Post</returns>
        public IList<Post> GetMostViewedPost(int size)
        {
            return _context.Posts.OrderByDescending(x => x.ViewCount).Take(size).ToList();
        }
        /// <summary>
        /// v
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Post</returns>
        public IList<Post> GetHighestPosts(int size)
        {
            return _context.Posts.OrderByDescending(x => x.Rate).Take(size).ToList();
        }

        public IEnumerable<Post> GetAllPostTagCategory()
        {
            return Context.Posts.Include(x => x.PostTagMaps)
                .ThenInclude(x => x.Tags)
                .Include(x => x.Category);
        }

        public Post FindPostBySlug(string slug)
        {
            return _context.Posts.Where(x => x.UrlSlug == slug).FirstOrDefault();
        }

        public IEnumerable<Post> GetPostsTagById(int? id)
        {
            var query = (from p in _context.Posts
                         join pt in _context.PostTagMaps
                         on p.Id equals pt.PostId
                         join t in _context.Tags
                         on pt.TagId equals t.Id
                         where pt.PostId == id
                         select p).Include(x => x.PostTagMaps)
                    .ThenInclude(x => x.Tags)
                    .Include(x => x.Category);
            return query.ToList();
        }
    }
}
