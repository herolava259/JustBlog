using AutoMapper;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.Model.Category;
using FA.JustBlog.Model.Comment;
using FA.JustBlog.Model.Post;
using FA.JustBlog.Model.PostTagMap;
using FA.JustBlog.Model.Tag;

namespace FA.JustBlog.Service.MapConfig
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Post, PostModel>().ReverseMap();

            CreateMap<Post, PostCategoryModel>().ForMember(p => p.PostId, s => s.MapFrom(s => s.Id))
                .ForMember(p => p.PostUrlSlug, s => s.MapFrom(s => s.UrlSlug))
                .ForMember(p => p.Title, s => s.MapFrom(s => s.Title))
                .ForMember(p => p.ShortDescription, s => s.MapFrom(s => s.ShortDescription))
                .ForMember(p => p.PostContent, s => s.MapFrom(s => s.PostContent))
                .ForMember(p => p.Published, s => s.MapFrom(s => s.Published))
                .ForMember(p => p.PostedOn, s => s.MapFrom(s => s.PostedOn))
                .ForMember(p => p.Modified, s => s.MapFrom(s => s.Modified))
                .ForMember(p => p.CategoryId, s => s.MapFrom(s => s.CategoryId))
                .ForMember(p => p.ViewCount, s => s.MapFrom(s => s.ViewCount))
                .ForMember(p => p.RateCount, s => s.MapFrom(s => s.RateCount))
                .ForMember(p => p.TotalRate, s => s.MapFrom(s => s.TotalRate))
                .ForMember(p => p.Category, s => s.MapFrom(s => s.Category))
                .ForMember(p => p.Rate, s => s.MapFrom(s => s.Rate)).ReverseMap();
            CreateMap<Category, PostCategoryModel>().ForMember(p => p.CateId, s => s.MapFrom(s => s.Id))
                .ForMember(p => p.Name, s => s.MapFrom(s => s.Name))
                .ForMember(p => p.CateUrlSlug, s => s.MapFrom(s => s.UrlSlug))
                .ForMember(p => p.Description, s => s.MapFrom(s => s.Description)).ReverseMap();
            CreateMap<Category, CreateCategoryModel>().ForMember(p => p.Name, s => s.MapFrom(s => s.Name))
               .ForMember(p => p.Description, s => s.MapFrom(s => s.Description))
               .ForMember(p => p.UrlSlug, s => s.MapFrom(s => s.UrlSlug)).ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();
            CreateMap<Post, CreatePostModel>().ReverseMap();
            CreateMap<Tag, TagModel>().ReverseMap();
            CreateMap<Post, PostModelView>().ReverseMap();
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<PostTagMap, PostTagMapModel>().ReverseMap();
        }
    }
}