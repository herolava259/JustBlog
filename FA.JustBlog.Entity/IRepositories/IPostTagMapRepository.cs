using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;

namespace FA.JustBlog.Entity.IRepositories
{
    public interface IPostTagMapRepository : IBaseRepository<PostTagMap>
    {
        IEnumerable<PostTagMap> GetPostTagMapsByPostId(int id);
        PostTagMap GetPostTagMapTagId(int id);
        IList<int> GetAllTagIdByPostId(int id);
        void UpdatePostTag(PostTagMap postTagMap);
    }
}
