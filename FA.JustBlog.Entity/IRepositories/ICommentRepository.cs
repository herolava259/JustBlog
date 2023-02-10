using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;

namespace FA.JustBlog.Entity.IRepositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody);
        IEnumerable<Comment> GetCommentsForPost(int postId);
        IEnumerable<Comment> GetCommentsForPost(Post post);
    }
}
