using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Model.Post
{
    public class PostModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PostedOn { get; set; }
        public int ViewCount { get; set; }
        public int RateCount { get; set; }
        public int TotalRate { get; set; }

        public decimal Rate { get => RateCount == 0 ? 0 : (TotalRate / RateCount); }
        public CategoryModel CategoryModel { get; set; }
        public IList<TagModel> TagModel { get; set; }

    }
}
