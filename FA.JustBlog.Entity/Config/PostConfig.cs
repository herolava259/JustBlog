using FA.JustBlog.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Entity.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ShortDescription).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PostContent).IsRequired().HasMaxLength(100);
            builder.Property(x => x.UrlSlug).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Published).HasDefaultValue(false);
            builder.Property(x => x.Modified).HasDefaultValue(false);
            builder.Property(x => x.PostedOn).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.Category)
                    .WithMany(x => x.Posts)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Post");

        }
    }
}
