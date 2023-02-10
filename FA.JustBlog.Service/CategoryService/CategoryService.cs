using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.Respond;
using FA.JustBlog.Common;
using FA.JustBlog.Entity.Entity;
using AutoMapper;

namespace FA.JustBlog.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Response<string> CreateCategory(CreateCategoryModel model)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    res.ErrorMessage = "Category name is required!";
                    return res;
                }

                var data = _unitOfWork.CategoryRepository.GetAll().FirstOrDefault(x => x.Name.Trim() == model.Name.Trim());
                if (data != null)
                {
                    res.ErrorMessage = "Category name is already exsist!";
                    return res;
                }
                CreateCategoryModel category = new CreateCategoryModel();
                category.Name = model.Name;
                category.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Name);
                category.Description = model.Description;

                _unitOfWork.CategoryRepository.Create(_mapper.Map<Category>(category));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "This category has been added successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while adding this category. Error code: " + ex.Message;
            }

            return res;
        }
        public Response<string> DeleteCategory(UpdateCategoryModel model)
        {
            Response<string> res = new Response<string>();

            try
            {

                _unitOfWork.CategoryRepository.Delete(model.Id);
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "This category had been deleted successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while deleting this category. Error code: " + ex.Message;
            }

            return res;
        }
        public Response<UpdateCategoryModel> FindById(int? id)
        {
            try
            {
                var post = _unitOfWork.CategoryRepository.GetById(id);
                var m = _mapper.Map<UpdateCategoryModel>(post);
                return new Response<UpdateCategoryModel>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<UpdateCategoryModel>(ex.Message + ex.InnerException.Message);
            }
        }
        public Response<List<PostCategoryModel>> GetAllCategory()
        {
            try
            {
                var post = _unitOfWork.CategoryRepository.GetAll().ToList();
                var m = _mapper.Map<List<PostCategoryModel>>(post);
                return new Response<List<PostCategoryModel>>() { Data = m };
            }
            catch (Exception ex)
            {
                return new Response<List<PostCategoryModel>>(ex.Message + ex.InnerException.Message);
            }
        }
        public Response<List<CategoryModel>> GetCategory()
        {
            try
            {
                var post = _unitOfWork.CategoryRepository.GetAll().ToList();
                var mapper = _mapper.Map<List<CategoryModel>>(post);
                return new Response<List<CategoryModel>>() { Data = mapper };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoryModel>>(ex.Message + ex.InnerException.Message);
            }
        }
        public Response<string> UpdateCategory(UpdateCategoryModel model)
        {
            Response<string> res = new Response<string>();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    res.ErrorMessage = "Category name is required!";
                    return res;
                }

                UpdateCategoryModel category = new UpdateCategoryModel();
                category.Id = model.Id;
                category.Name = model.Name;
                category.UrlSlug = SeoUrlHelper.FrientlyUrl(model.Name);
                category.Description = model.Description;

                _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(category));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "This category had been updated successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error while updating this category";
            }

            return res;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
