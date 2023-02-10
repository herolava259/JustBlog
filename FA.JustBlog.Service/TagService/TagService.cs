using AutoMapper;
using FA.JustBlog.Common;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Model.Respond;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Service.TagService
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Response<string> CreateTag(TagModel model)
        {
            Response<string> res = new Response<string>();

            try
            {
                var data = _unitOfWork.TagRepository.GetAll().FirstOrDefault(x => x.Name.Trim() == model.Name.Trim());
                if (data != null)
                {
                    res.ErrorMessage = "Tag name is already existed!";
                    return res;
                }

                TagModel tag = new TagModel();
                tag.Name = model.Name;
                tag.Description = model.Description;
                if (model.UrlSlug != null)
                {
                    tag.UrlSlug = model.UrlSlug;
                }
                else
                {
                    tag.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Name);
                }
                tag.Count = model.Count;

                _unitOfWork.TagRepository.Create(_mapper.Map<Tag>(tag));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Tag has been added successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error, code:" + ex.Message;
            }

            return res;
        }
        public Response<string> DeleteTag(TagModel model)
        {
            Response<string> res = new Response<string>();
            try
            {

                _unitOfWork.TagRepository.Delete(model.Id);
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Tag has been deleted successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error, code: " + ex.Message;
            }
            return res;
        }
        public Response<TagModel> FindById(int? id)
        {
            try
            {
                var tag = _unitOfWork.TagRepository.GetById(id);
                var mapping = _mapper.Map<TagModel>(tag);
                return new Response<TagModel>() { Data = mapping };
            }
            catch (Exception ex)
            {
                return new Response<TagModel>("There are an error, code: " + ex.Message);
            }
        }
        public Response<List<TagModel>> GetAllTag()
        {
            try
            {
                var tag = _unitOfWork.TagRepository.GetAll().ToList();
                var m = _mapper.Map<List<TagModel>>(tag);
                return new Response<List<TagModel>>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<List<TagModel>>("There are an error, code: " + ex.Message);
            }
        }
        public Response<List<TagModel>> GetTagMapByPostId(int? id)
        {
            Response<List<TagModel>> res = new Response<List<TagModel>>();
            List<TagModel> list = new List<TagModel>();

            try
            {
                var tags = _unitOfWork.TagRepository.GetTagMapByPostId(id).ToList();
                if (tags == null)
                {
                    res.ErrorMessage = "Tag not found";
                    return res;
                }

                foreach (var item in tags)
                {
                    var model = new TagModel();
                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.Description = item.Description;
                    model.UrlSlug = item.UrlSlug;
                    model.Count = item.Count;
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
        public Response<string> UpdateTag(TagModel model)
        {
            Response<string> res = new Response<string>();
            try
            {
                TagModel tag = new TagModel();
                tag.Id = model.Id;
                tag.Name = model.Name;
                tag.Description = model.Description;
                if (model.UrlSlug != null)
                {
                    tag.UrlSlug = model.UrlSlug;
                }
                else
                {
                    tag.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Name);
                }
                tag.Count = model.Count;

                _unitOfWork.TagRepository.Update(_mapper.Map<Tag>(tag));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "This tag has been updated successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error, code: " + ex.Message;
            }
            return res;
        }
    }
}

