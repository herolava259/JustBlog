using FA.JustBlog.Entity.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Entity.Config
{
    public class PostTagMapConfig : IEntityTypeConfiguration<PostTagMap>
    {
        public void Configure(EntityTypeBuilder<PostTagMap> builder)
        {
            builder.ToTable("PostTagMap");
            builder.HasKey(x => new { x.TagId, x.PostId });

            builder.HasOne(x => x.Tags)
                    .WithMany(x => x.PostTagMaps)
                    .HasForeignKey(x => x.TagId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tags_PostTagMap");

            builder.HasOne(x => x.Post)
                    .WithMany(x => x.PostTagMaps)
                    .HasForeignKey(x => x.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Post_PostTagMap");
        }
    }
}
