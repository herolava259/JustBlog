using FA.JustBlog.Model.Post;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Model.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PostModel> PostModels { get; set; }
    }
}
