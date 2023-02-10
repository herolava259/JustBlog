using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Entity.IRepositories;

namespace FA.JustBlog.Entity.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly JustBlogContext _context;

        public CommentRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// AddComment by postid, name, email, header, body
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="commentName"></param>
        /// <param name="commentEmail"></param>
        /// <param name="commentTitle"></param>
        /// <param name="commentBody"></param>
        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            _context.Comments.Add(new Comment
            {
                PostId = postId,
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody
            });
            _context.SaveChanges();
        }
        /// <summary>
        /// GetCommentsForPost
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Comment</returns>
        public IEnumerable<Comment> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(x => x.PostId==postId).ToList();
        }
        /// <summary>
        /// GetCommentsForPost
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Comment</returns>
        public IEnumerable<Comment> GetCommentsForPost(Post post)
        {
            return _context.Comments.Where(x => x.PostId == post.Id).ToList();
        }
    }
}
