using AutoMapper;
using FA.JustBlog.Common;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.PostTagMap;
using FA.JustBlog.Model.Respond;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Service.PostService
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public Response<PostModel> FindPost(int year, int month, string urlSlug)
        {
            try
            {
                var post = _unitOfWork.PostRepository.FindPost(year, month, urlSlug);
                var m = _mapper.Map<PostModel>(post);
                return new Response<PostModel>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<PostModel>(ex.Message + ex.InnerException.Message);
            }
        }

        //public Response<List<PostCategoryModel>> GetAllPost()
        //{
        //    //try
        //    //{
        //    //    var post = _unitOfWork.PostRepository.GetAll().ToList();
        //    //    var m = _mapper.Map<List<PostCategoryModel>>(post);
        //    //    return new Response<List<PostCategoryModel>>() { Data = m};
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return new Response<List<PostCategoryModel>>(ex.Message + ex.InnerException.Message);
        //    //}


        //    Response<List<PostCategoryModel>> res = new Response<List<PostCategoryModel>>();
        //    List<PostCategoryModel> list = new List<PostCategoryModel>();

        //    try
        //    {
        //        var posts = _unitOfWork.PostRepository.GetAll().ToList();
        //        if (posts == null)
        //        {
        //            res.ErrorMessage = "Khong tim thay post";
        //            return res;
        //        }

        //        foreach (var item in posts)
        //        {
        //            var model = new PostCategoryModel();

        //            model.PostId = item.Id;
        //            model.TagsModels = _mapper.Map<IList<TagsModel>>(item.PostTagMaps.Select(x => x.Tags).ToList());
        //            model.Category = _mapper.Map<CategoryModel>(item.Category);
        //            model.ShortDescription = item.ShortDescription;
        //            model.PostedOn = item.PostedOn;
        //            model.PostUrlSlug = item.UrlSlug;
        //            model.Title = item.Title;
        //            list.Add(model);
        //        }
        //        res.Data = list;
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMessage = ex.Message;
        //    }
        //    return res;
        //}

        public Response<List<PostModel>> GetLatestPost(int size)
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetLatestPost(size).ToList();
                var m = _mapper.Map<List<PostModel>>(post);
                return new Response<List<PostModel>>() { Data = m };

            }
            catch (Exception ex)
            {

                return new Response<List<PostModel>>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<List<PostModel>> GetMostViewedPost(int size)
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetMostViewedPost(size).ToList();
                var m = _mapper.Map<List<PostModel>>(post);
                return new Response<List<PostModel>>() { Data = m };

            }
            catch (Exception ex)
            {

                return new Response<List<PostModel>>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<List<PostModel>> GetPost()
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetAll().ToList();
                var m = _mapper.Map<List<PostModel>>(post);
                return new Response<List<PostModel>>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<List<PostModel>>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<PostModel> GetPostById(int? id)
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetById(id);
                var m = _mapper.Map<PostModel>(post);
                return new Response<PostModel>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<PostModel>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<List<CreatePostModel>> GetPostCreatePostModel()
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetAll().ToList();
                var m = _mapper.Map<List<CreatePostModel>>(post);
                return new Response<List<CreatePostModel>>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<List<CreatePostModel>>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<List<PostModelView>> GetPostsByCategory(string category)
        {
            Response<List<PostModelView>> res = new Response<List<PostModelView>>();
            List<PostModelView> list = new List<PostModelView>();

            try
            {
                var posts = _unitOfWork.PostRepository.GetPostsByCategory(category).ToList();
                if (posts == null)
                {
                    res.ErrorMessage = "Cannot find the post!";
                    return res;
                }

                foreach (var item in posts)
                {
                    var model = new PostModelView();

                    model.Id = item.Id;
                    model.TagModel = _mapper.Map<IList<TagModel>>(item.PostTagMaps.Select(x => x.Tags).ToList());
                    model.CategoryModel = _mapper.Map<CategoryModel>(item.Category);
                    model.ShortDescription = item.ShortDescription;
                    model.PostedOn = item.PostedOn;
                    model.UrlSlug = item.UrlSlug;
                    model.Title = item.Title;
                    model.ViewCount = item.ViewCount;
                    model.TotalRate = item.TotalRate;
                    model.RateCount = item.RateCount;
                    list.Add(model);
                }
                res.Data = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }



        public Response<List<PostModelView>> GetPostsByTag(string tag)
        {
            Response<List<PostModelView>> res = new Response<List<PostModelView>>();
            List<PostModelView> list = new List<PostModelView>();

            try
            {
                var tags = _unitOfWork.PostRepository.GetPostsByTag(tag).ToList();
                if (tags == null)
                {
                    res.ErrorMessage = "Cannot find the post!";
                    return res;
                }

                foreach (var item in tags)
                {
                    var model = new PostModelView();

                    model.Id = item.Id;
                    model.TagModel = _mapper.Map<IList<TagModel>>(item.PostTagMaps.Select(x => x.Tags).ToList());
                    model.CategoryModel = _mapper.Map<CategoryModel>(item.Category);
                    model.ShortDescription = item.ShortDescription;
                    model.PostedOn = item.PostedOn;
                    model.UrlSlug = item.UrlSlug;
                    model.Title = item.Title;
                    model.ViewCount = item.ViewCount;
                    model.TotalRate = item.TotalRate;
                    model.RateCount = item.RateCount;
                    list.Add(model);
                }
                res.Data = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public Response<List<PostModelView>> GetAllPostView()
        {
            Response<List<PostModelView>> res = new Response<List<PostModelView>>();
            List<PostModelView> list = new List<PostModelView>();
            try
            {
                var posts = _unitOfWork.PostRepository.GetAllPostTagCategory().ToList();
                if (posts is null)
                {
                    res.ErrorMessage = "Cannot find this post!";
                    return res;
                }

                foreach (var item in posts)
                {
                    var model = new PostModelView();

                    model.Id = item.Id;
                    model.TagModel = _mapper.Map<IList<TagModel>>(item.PostTagMaps.Select(x => x.Tags).ToList());
                    model.CategoryModel = _mapper.Map<CategoryModel>(item.Category);
                    model.ShortDescription = item.ShortDescription;
                    model.PostedOn = item.PostedOn;
                    model.UrlSlug = item.UrlSlug;
                    model.Title = item.Title;
                    model.ViewCount = item.ViewCount;
                    model.TotalRate = item.TotalRate;
                    model.RateCount = item.RateCount;
                    list.Add(model);
                }
                res.Data = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }
        public Response<string> CreatePost(PostModel model, IEnumerable<int> listTagsId)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (string.IsNullOrEmpty(model.Title) || string.IsNullOrWhiteSpace(model.Title))
                {
                    res.ErrorMessage = "Post name is required!";
                    return res;
                }

                var data = _unitOfWork.PostRepository.GetAll().FirstOrDefault(x => x.Title.Trim() == model.Title.Trim());
                if (data != null)
                {
                    res.ErrorMessage = "This title is exsited!";
                    return res;
                }
                if (model != null)
                {
                    CreatePostModel post = new CreatePostModel();
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.PostContent = model.PostContent;

                    post.Modified = false;
                    if (model.UrlSlug != null)
                    {
                        post.UrlSlug = model.UrlSlug;
                    }
                    else
                    {
                        post.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Title);
                    }
                    if (model.Published == null)
                    {
                        post.Published = true;
                    }
                    else
                    {
                        post.Published = model.Published;
                    }
                    if (post.PostedOn != null)
                    {
                        post.PostedOn = model.PostedOn;

                    }
                    else
                    {
                        post.PostedOn = DateTime.Now;
                    }
                    post.CategoryId = model.CategoryId;

                    _unitOfWork.PostRepository.Create(_mapper.Map<Post>(post));
                    _unitOfWork.SaveChanges();

                    var m = _unitOfWork.PostRepository.FindPostBySlug(post.UrlSlug);
                    foreach (var id in listTagsId)
                    {
                        PostTagMapModel item = new PostTagMapModel();
                        item.TagId = id;
                        item.PostId = m.Id;
                        _unitOfWork.PostTagMapRepository.Create(_mapper.Map<PostTagMap>(item));
                    }

                    res.ErrorMessage = "Post has been added successfully!";
                }
                _unitOfWork.SaveChanges();
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while adding this post. Code: " + ex.Message;
                //res.ErrorMessage= ex.Message;
            }

            return res;
        }
        Response<List<PostModel>> IPostService.GetPostTagById(int? id)
        {
            Response<List<PostModel>> res = new Response<List<PostModel>>();
            List<PostModel> list = new List<PostModel>();

            try
            {
                var tags = _unitOfWork.PostRepository.GetPostsTagById(id).ToList();
                if (tags == null)
                {
                    res.ErrorMessage = "Cannot find this post";
                    return res;
                }

                foreach (var item in tags)
                {
                    var model = new PostModel();
                    model.Id = item.Id;
                    model.TagsModels = _mapper.Map<IList<TagModel>>(item.PostTagMaps.Select(x => x.Tags).ToList());
                    model.CategoryModel = _mapper.Map<CategoryModel>(item.Category);
                    model.ShortDescription = item.ShortDescription;
                    model.PostedOn = item.PostedOn;
                    model.UrlSlug = item.UrlSlug;
                    model.Title = item.Title;
                    model.ViewCount = item.ViewCount;
                    model.TotalRate = item.TotalRate;
                    model.RateCount = item.RateCount;
                    list.Add(model);
                }
                res.Data = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public Response<string> UpdatePost(PostModel model, IEnumerable<int> listTagsId)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    res.ErrorMessage = "Title is required!";
                    return res;
                }
                if (model != null)
                {

                    PostModel post = new PostModel();
                    post.Id = model.Id;
                    post.Title = model.Title;
                    post.ShortDescription = model.ShortDescription;
                    post.PostContent = model.PostContent;
                    post.Modified = false;
                    if (model.UrlSlug != null)
                    {
                        post.UrlSlug = model.UrlSlug;
                    }
                    else
                    {
                        post.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Title);
                    }
                    if (model.Published == null)
                    {
                        post.Published = true;
                    }
                    else
                    {
                        post.Published = model.Published;
                    }
                    if (post.PostedOn != null)
                    {
                        post.PostedOn = model.PostedOn;

                    }
                    else
                    {
                        post.PostedOn = DateTime.Now;
                    }
                    post.CategoryId = model.CategoryId;

                    _unitOfWork.PostRepository.Update(_mapper.Map<Post>(post));
                    _unitOfWork.SaveChanges();

                    var posttagmap = _unitOfWork.PostTagMapRepository.GetPostTagMapsByPostId(model.Id);
                    if (posttagmap != null)
                    {
                        foreach (var item in posttagmap)
                        {
                            _unitOfWork.PostTagMapRepository.Delete(item);
                        }
                        _unitOfWork.SaveChanges();
                    }

                    var m = _unitOfWork.PostRepository.FindPostBySlug(post.UrlSlug);
                    if (m != null)
                    {
                        foreach (var id in listTagsId)
                        {
                            PostTagMapModel item = new PostTagMapModel();
                            item.TagId = id;
                            item.PostId = m.Id;
                            _unitOfWork.PostTagMapRepository.Create(_mapper.Map<PostTagMap>(item));
                        }
                        _unitOfWork.SaveChanges();
                    }
                    res.ErrorMessage = "Post has been updated successfully!";
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while updating this post, Code:" + ex.Message;
            }
            return res;
        }

        public Response<string> DeletePost(PostModel model)
        {
            Response<string> res = new Response<string>();

            try
            {
                _unitOfWork.PostRepository.Delete(model.Id);
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Post had been deleted successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while update this post, Code:" + ex.Message;
            }

            return res;
        }

        public Response<List<PostModel>> GetPublishedPost()
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetPublisedPosts().ToList();
                var m = _mapper.Map<List<PostModel>>(post);
                return new Response<List<PostModel>>() { Data = m };
            }
            catch (Exception ex)
            {

                return new Response<List<PostModel>>(ex.Message + ex.InnerException.Message);
            }
        }

        public Response<List<PostModel>> GetUnPublishedPost()
        {
            try
            {
                var post = _unitOfWork.PostRepository.GetUnpublisedPosts().ToList();
                var mapping = _mapper.Map<List<PostModel>>(post);
                return new Response<List<PostModel>>() { Data = mapping };

            }
            catch (Exception ex)
            {

                return new Response<List<PostModel>>("There are an error! code: " + ex.Message);
            }
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}