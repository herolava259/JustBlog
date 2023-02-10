using FA.JustBlog.Model.Post;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Model.Comment
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Post id is required!")]
        public int PostId { get; set; }
        public PostModel? PostModel { get; set; }
        [Required(ErrorMessage = "Comment header is required!")]
        public string CommentHeader { get; set; }
        [Required(ErrorMessage = "Comment content is required!")]
        public string CommentText { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CommentTime { get; set; }
    }
}
