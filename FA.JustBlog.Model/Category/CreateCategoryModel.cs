namespace FA.JustBlog.Model.Category
{
    public class CreateCategoryModel
    {
        public string Name { get; set; }
        public string? UrlSlug { get; set; }
        public string Description { get; set; }
    }
}
