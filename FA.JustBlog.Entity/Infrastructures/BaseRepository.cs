using FA.JustBlog.Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Entity.Infrastructures
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly JustBlogContext Context;
        protected DbSet<TEntity> DbSet;
        public BaseRepository(JustBlogContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(params object[] ids)
        {
            var entity = DbSet.Find(ids);
            if (entity == null)
                throw new ArgumentException($"{string.Join(";", ids)} not exist in the {typeof(TEntity).Name} table");
            DbSet.Remove(entity);
        }

        public IList<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public TEntity GetById(params object[] primaryKey)
        {
            return DbSet.Find(primaryKey);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}
