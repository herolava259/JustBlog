using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Model.Post
{
    public class PostCategoryModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string PostUrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public bool Modified { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { set; get; }
        public int ViewCount { get; set; }
        public int RateCount { get; set; }
        public int TotalRate { get; set; }

        public decimal Rate { get => RateCount == 0 ? 0 : (TotalRate / RateCount); }

        public int CateId { get; set; }
        public string Name { get; set; }
        public string CateUrlSlug { get; set; }
        public string Description { get; set; }
        public IList<TagModel> TagsModels { get; set; }
    }
}
