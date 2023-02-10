using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.IRepositories;

namespace FA.JustBlog.Entity.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public IPostRepository PostRepository { get; }
        public ITagRepository TagRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IPostTagMapRepository PostTagMapRepository { get; }
        public JustBlogContext JustBlogContext { get; }
        int SaveChanges();
    }
}
