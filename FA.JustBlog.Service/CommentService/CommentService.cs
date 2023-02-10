using AutoMapper;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Model.Comment;
using FA.JustBlog.Model.Respond;

namespace FA.JustBlog.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Response<string> CreateComment(CommentModel model)
        {
            Response<string> res = new Response<string>();

            try
            {
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    res.ErrorMessage = "Comment name is required!";
                    return res;
                }
                CommentModel comment = new CommentModel();
                comment.Name = model.Name;
                comment.Email= model.Email;
                comment.CommentText = model.CommentText;
                comment.CommentHeader = model.CommentHeader;
                comment.PostId = model.PostId;
                if (model.CommentTime != null)
                {
                    comment.CommentTime = model.CommentTime;
                }
                else
                {
                    comment.CommentTime = DateTime.Now;
                }
                _unitOfWork.CommentRepository.Create(_mapper.Map<Comment>(comment));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Comment has been added successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error. Code: " + ex.Message;
            }

            return res;
        }

        public Response<string> DeleteComment(CommentModel model)
        {
            Response<string> res = new Response<string>();
            try
            {
                _unitOfWork.CommentRepository.Delete(model.Id);
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Comment has been deleted successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error. Code: " + ex.Message;
            }

            return res;
        }

        public Response<CommentModel> FindById(int? id)
        {
            try
            {
                var tag = _unitOfWork.CommentRepository.GetById(id);
                var mapper = _mapper.Map<CommentModel>(tag);
                return new Response<CommentModel>() { Data = mapper };
            }
            catch (Exception ex)
            {
                return new Response<CommentModel>("There are an error, Code" + ex.Message);
            }
        }

        public Response<List<CommentModel>> GetAllComment()
        {
            try
            {
                var tag = _unitOfWork.CommentRepository.GetAll().ToList();
                var mapper = _mapper.Map<List<CommentModel>>(tag);
                return new Response<List<CommentModel>>() { Data = mapper };
            }
            catch (Exception ex)
            {
                return new Response<List<CommentModel>>("There are an error, Code: " + ex.Message );
            }
        }

        public Response<string> UpdateComment(CommentModel model)
        {
            Response<string> res = new Response<string>();
            try
            {
                CommentModel comment = new CommentModel();
                comment.Id = model.Id;
                comment.Name = model.Name;
                comment.Email = model.Email;
                comment.CommentText = model.CommentText;
                comment.CommentHeader = model.CommentHeader;
                comment.PostId = model.PostId;
                if (model.CommentTime != null)
                {
                    comment.CommentTime = model.CommentTime;
                }
                else
                {
                    comment.CommentTime = DateTime.Now;
                }

                _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(comment));
                _unitOfWork.SaveChanges();
                res.ErrorMessage = "Comment has been updated successfully!";
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = "There are an error. Code: " + ex.Message;
            }
            return res;
        }
    }
}
