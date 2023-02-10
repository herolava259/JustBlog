using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.IRepositories;
using FA.JustBlog.Entity.Repositories;

namespace FA.JustBlog.Entity.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JustBlogContext _context;
        private ICategoryRepository _categoryRepository;
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private ICommentRepository _commentRepository;
        public IPostTagMapRepository _postTagMapRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(JustBlogContext context)
        {
            _context = context;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));
        public IPostRepository PostRepository => _postRepository ?? (_postRepository = new PostRepository(_context));
        public ITagRepository TagRepository => _tagRepository ?? (_tagRepository = new TagsRepository(_context));
        public ICommentRepository CommentRepository => _commentRepository ?? (_commentRepository = new CommentRepository(_context));
        public IPostTagMapRepository PostTagMapRepository => _postTagMapRepository ?? (_postTagMapRepository = new PostTagMapRepository(_context));
        public JustBlogContext JustBlogContext => _context;
        public void Dispose()
        {
            _context.Dispose();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
