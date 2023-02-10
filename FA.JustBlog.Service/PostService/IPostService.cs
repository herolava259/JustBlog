using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.Respond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Service.PostService
{
    public interface IPostService
    {
        /// <summary>
        /// Get post with create post model
        /// </summary>
        /// <returns></returns>
        public Response<List<CreatePostModel>> GetPostCreatePostModel();
        /// <summary>
        /// Create a post
        /// </summary>
        /// <param name="model"></param>
        /// <param name="listTagsId"></param>
        /// <returns></returns>
        public Response<string> CreatePost(PostModel model, IEnumerable<int> listTagsId);
        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="model"></param>
        /// <param name="listTagsId"></param>
        /// <returns></returns>
        public Response<string> UpdatePost(PostModel model, IEnumerable<int> listTagsId);
        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> DeletePost(PostModel model);
        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<PostModel> GetPostById(int? id);
        /// <summary>
        /// Get a post
        /// </summary>
        /// <returns></returns>
        public Response<List<PostModel>> GetPost();
        /// <summary>
        /// Find a post with condition
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="urlSlug">url slug</param>
        /// <returns></returns>
        public Response<PostModel> FindPost(int year, int month, string urlSlug);
        /// <summary>
        /// Get the most viewed post
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Response<List<PostModel>> GetMostViewedPost(int size);
        /// <summary>
        /// Get the latest post
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Response<List<PostModel>> GetLatestPost(int size);
        /// <summary>
        /// Get the latest post
        /// </summary>
        /// <returns></returns>
        public Response<List<PostModel>> GetPublishedPost();
        /// <summary>
        /// Get the unpublished post
        /// </summary>
        /// <returns></returns>
        public Response<List<PostModel>> GetUnPublishedPost();
        /// <summary>
        /// Get post by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Response<List<PostModelView>> GetPostsByCategory(string category);
        /// <summary>
        /// Get post by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Response<List<PostModelView>> GetPostsByTag(string tag);
        /// <summary>
        /// Get post tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<List<PostModel>> GetPostTagById(int? id);
        /// <summary>
        /// Get all post view
        /// </summary>
        /// <returns></returns>
        public Response<List<PostModelView>> GetAllPostView();
        /// <summary>
        /// Just dispose
        /// </summary>
        void Dispose();
    }
}
