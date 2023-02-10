using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;

namespace FA.JustBlog.Entity.IRepositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Post FindPost(int year, int month, string urlSlug);
        Post FindPostBySlug(string slug);
        IEnumerable<Post> GetPublisedPosts();
        IEnumerable<Post> GetUnpublisedPosts();
        IEnumerable<Post> GetLatestPost(int size);
        IEnumerable<Post> GetPostsByMonth(DateTime monthYear);
        int CountPostsForCategory(string category);
        IEnumerable<Post> GetPostsByCategory(string category);
        int CountPostsForTag(string tag);
        IEnumerable<Post> GetPostsByTag(string tag);
        IEnumerable<Post> GetPostsTagById(int? id);
        IList<Post> GetMostViewedPost(int size);
        IList<Post> GetHighestPosts(int size);
        IEnumerable<Post> GetAllPostTagCategory();
    }
}
