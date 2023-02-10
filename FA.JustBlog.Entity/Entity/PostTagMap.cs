namespace FA.JustBlog.Entity.Entity
{
    public class PostTagMap
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public Post Post { set; get; }
        public Tag Tags { set; get; }
    }
}
