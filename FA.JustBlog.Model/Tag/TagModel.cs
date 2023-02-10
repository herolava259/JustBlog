using FA.JustBlog.Model.PostTagMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model.Tag
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UrlSlug { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public virtual ICollection<PostTagMapModel>? PostTagMapModel { get; set; }

    }
}
