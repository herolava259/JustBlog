using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;

namespace FA.JustBlog.Entity.IRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Tag GetTagByUrlSlug(string urlSlug);
        IEnumerable<Tag> GetTagMapByPostId(int? id);
    }
}
