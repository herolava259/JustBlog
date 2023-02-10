using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Model.Category
{
    public class UpdateCategoryModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        public string? UrlSlug { get; set; }
        public string Description { get; set; }
    }
}
