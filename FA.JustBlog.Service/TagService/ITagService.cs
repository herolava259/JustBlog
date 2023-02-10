using FA.JustBlog.Model.Respond;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Service.TagService
{
    public interface ITagService
    {
        /// <summary>
        /// Get all the tags
        /// </summary>
        /// <returns></returns>
        public Response<List<TagModel>> GetAllTag();
        /// <summary>
        /// Get the tag map by the post id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<List<TagModel>> GetTagMapByPostId(int? id);
        /// <summary>
        /// Create a tag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> CreateTag(TagModel model);
        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> UpdateTag(TagModel model);
        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> DeleteTag(TagModel model);
        /// <summary>
        /// Find tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<TagModel> FindById(int? id);
    }
}
