using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Entity.IRepositories;

namespace FA.JustBlog.Entity.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(JustBlogContext context) : base(context)
        {

        }
    }
}
