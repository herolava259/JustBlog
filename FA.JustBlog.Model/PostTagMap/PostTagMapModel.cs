using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Model.PostTagMap
{
    public class PostTagMapModel
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public PostModel PostModels { set; get; }
        public TagModel TagsModels { set; get; }
    }
}
