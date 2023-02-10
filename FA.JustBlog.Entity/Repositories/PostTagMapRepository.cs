using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Entity.Infrastructures;
using FA.JustBlog.Entity.IRepositories;

namespace FA.JustBlog.Entity.Repositories
{
    public class PostTagMapRepository : BaseRepository<PostTagMap>, IPostTagMapRepository
    {
        public PostTagMapRepository(JustBlogContext context) : base(context)
        {
        }

        public IList<int> GetAllTagIdByPostId(int id)
        {
            var data = (from pt in Context.PostTagMaps
                        where pt.PostId.Equals(id)
                        select pt.Tags.Id).ToList();
            return data;
        }

        public IEnumerable<PostTagMap> GetPostTagMapsByPostId(int id)
        {
            var data = (from pt in Context.PostTagMaps
                        where pt.PostId.Equals(id)
                        select pt).ToList();
            return data;
        }

        public PostTagMap GetPostTagMapTagId(int id)
        {
            var data = (from pt in Context.PostTagMaps
                        where pt.TagId.Equals(id)
                        select pt).FirstOrDefault();
            return data;
        }

        public void UpdatePostTag(PostTagMap postTagMap)
        {
            //if (postTagMap != null)
            //{
            //    var data = Context.PostTagMaps.Where(p => p.Id == postTagMap.Id).FirstOrDefault();
            //    if (data != null)
            //    {
            //        data.TagId = postTagMap.TagId;
            //        Context.SaveChanges();
            //    }
            //}
        }
    }
}
