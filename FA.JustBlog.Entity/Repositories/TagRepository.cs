using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Entity.IRepositories;

namespace FA.JustBlog.Entity.Repositories
{
    public class TagsRepository : BaseRepository<Tag>, ITagRepository
    {
        private readonly JustBlogContext _context;

        public TagsRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// GetTagByUrlSlug
        /// </summary>
        /// <param name="urlSlug"></param>
        /// <returns>Tags</returns>
        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return _context.Tags.Where(t => t.UrlSlug == urlSlug).FirstOrDefault();
        }

        public IEnumerable<Tag> GetTagMapByPostId(int? id)
        {
            var query = (from t in _context.Tags
                         join pt in _context.PostTagMaps
                         on t.Id equals pt.TagId
                         where pt.PostId == id
                         select t);
            return query.ToList();
        }
    }
}
