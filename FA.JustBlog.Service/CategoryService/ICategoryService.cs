using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.Respond;

namespace FA.JustBlog.Service.CategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all the category
        /// </summary>
        /// <returns></returns>
        public Response<List<PostCategoryModel>> GetAllCategory();
        /// <summary>
        /// Get category
        /// </summary>
        /// <returns></returns>
        public Response<List<CategoryModel>> GetCategory();
        /// <summary>
        /// Create the category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> CreateCategory(CreateCategoryModel model);
        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> UpdateCategory(UpdateCategoryModel model);
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> DeleteCategory(UpdateCategoryModel model);
        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<UpdateCategoryModel> FindById(int? id);
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose();
    }
}
