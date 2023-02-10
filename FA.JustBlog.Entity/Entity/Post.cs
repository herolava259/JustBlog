using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Entity.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public bool Modified { get; set; }
        public int CategoryId { get; set; }
        public Category Category { set; get; }

        public int ViewCount { get; set; }
        public int RateCount { get; set; }
        public int TotalRate { get; set; }

        [NotMapped]
        public decimal Rate { get => RateCount == 0 ? 0 : (TotalRate / RateCount); }

        public virtual ICollection<PostTagMap> PostTagMaps { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
