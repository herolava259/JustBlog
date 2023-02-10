using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Comment;
using FA.JustBlog.Model.PostTagMap;
using FA.JustBlog.Model.Tag;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Model.Post
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string? UrlSlug { get; set; }
        public bool? Published { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PostedOn { get; set; }
        public bool? Modified { get; set; }
        public int? CategoryId { get; set; }
        public CategoryModel? CategoryModel { set; get; }
        public int ViewCount { get; set; }
        public int RateCount { get; set; }
        public int TotalRate { get; set; }

        public decimal Rate { get => RateCount == 0 ? 0 : (TotalRate / RateCount); }

        public virtual ICollection<PostTagMapModel>? PostTagMapModels { get; set; }
        public virtual ICollection<CommentModel>? CommentModels { get; set; }
        public IList<TagModel>? TagsModels { get; set; }
        public ICollection<TagModel>? SelectedTagModels { get; set; }

    }
}
