using FA.JustBlog.Entity.Config;
using FA.JustBlog.Entity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Entity.Context
{
    public class JustBlogContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JustBlogContext()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public JustBlogContext(DbContextOptions<JustBlogContext> options) : base(options)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
            //Remove AspNet table prefix: default
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName[6..]);
                }
            }
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTagMap> PostTagMaps { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
