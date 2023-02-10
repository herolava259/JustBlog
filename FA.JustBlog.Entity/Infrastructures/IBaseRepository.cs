namespace FA.JustBlog.Entity.Infrastructures
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetById(params object[] primaryKey);

        /// <summary>
        /// Change state of entity to added
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);

        /// <summary>
        /// Change state of entity to modified
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(params object[] ids);

        //IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        /// <summary>
        /// Get all <paramref name="TEntity"></paramref> from database
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();
    }
}
