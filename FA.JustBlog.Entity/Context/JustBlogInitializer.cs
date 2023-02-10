using FA.JustBlog.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Entity.Context
{
    public static class JustBlogInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Category 1",
                    Description = "This is category 1",
                    UrlSlug = "category_1"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Category 2",
                    Description = "This is category 2",
                    UrlSlug = "category_2"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Category 3",
                    Description = "This is category 3",
                    UrlSlug = "category_3"
                }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post()
                {
                    Id = 1,
                    UrlSlug = "post_slug_1",
                    PostContent = "PostContent 1",
                    ShortDescription = "ShortDescription 1",
                    Title = "Title 1",
                    CategoryId = 1,
                    RateCount = 2,
                    TotalRate = 2
                },
                new Post()
                {
                    Id = 2,
                    UrlSlug = "post_slug_2",
                    PostContent = "PostContent 2",
                    ShortDescription = "ShortDescription 2",
                    Title = "Title 2",
                    CategoryId = 2,
                    RateCount = 3,
                    TotalRate = 3
                },
                new Post()
                {
                    Id = 3,
                    UrlSlug = "post_slug_3",
                    PostContent = "PostContent 3",
                    ShortDescription = "ShortDescription 3",
                    Title = "Title 3",
                    CategoryId = 3,
                    RateCount = 1,
                    TotalRate = 1
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag()
                {
                    Id = 1,
                    Count = 1,
                    UrlSlug = "tags_1",
                    Description = "This is tag 1",
                    Name = "Tag 1"
                },
                new Tag()
                {
                    Id = 2,
                    Count = 2,
                    UrlSlug = "tags_2",
                    Description = "This is tag 2",
                    Name = "Tag 2"
                },
                new Tag()
                {
                    Id = 3,
                    Count = 3,
                    UrlSlug = "tags_3",
                    Description = "This is tag 3",
                    Name = "Tag 2"
                }
                );

            modelBuilder.Entity<PostTagMap>().HasData(
            new PostTagMap()
            {
                PostId = 1,
                TagId = 1
            },
            new PostTagMap()
            {
                PostId = 2,
                TagId = 2
            },
            new PostTagMap()
            {
                PostId = 3,
                TagId = 3
            });

            modelBuilder.Entity<Comment>().HasData(
                new Comment()
                {
                    Id=1,
                    Name ="Comment 1",
                    PostId = 1,
                    Email="abc@gmail.com",
                    CommentHeader="Header 1",
                    CommentText="Text 1"

                },
                new Comment()
                {
                    Id = 2,
                    Name = "Comment 2",
                    PostId = 2,
                    Email = "abcd@gmail.com",
                    CommentHeader = "Header 2",
                    CommentText = "Text 2"
                },
                new Comment()
                {
                    Id = 3,
                    Name = "Comment 3",
                    PostId = 3,
                    Email = "abce@gmail.com",
                    CommentHeader = "Header 3",
                    CommentText = "Text 3"
                });
        }
    }
}
