using FA.JustBlog.Model.Comment;
using FA.JustBlog.Model.Respond;

namespace FA.JustBlog.Service.CommentService
{
    public interface ICommentService
    {
        /// <summary>
        /// Get all the comment
        /// </summary>
        /// <returns></returns>
        public Response<List<CommentModel>> GetAllComment();
        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> CreateComment(CommentModel model);
        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> UpdateComment(CommentModel model);
        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> DeleteComment(CommentModel model);
        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<CommentModel> FindById(int? id);
    }
}
