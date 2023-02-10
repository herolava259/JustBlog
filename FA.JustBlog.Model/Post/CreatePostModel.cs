using FA.JustBlog.Model.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FA.JustBlog.Model.Post
{
    public class CreatePostModel
    {
        [Display(Name = "Tên tiêu đề")]
        [Required(ErrorMessage = "Bạn phải nhập tiêu đề")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập mô tả")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập nội dung")]
        public string PostContent { get; set; }
        public string? UrlSlug { get; set; }
        public bool? Published { get; set; }
        public DateTime? PostedOn { get; set; }
        public bool? Modified { get; set; }
        public int? CategoryId { get; set; }
        public CategoryModel? Category { set; get; }
        public int? ViewCount { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập rate count")]
        public int RateCount { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập total rate")]
        public int TotalRate { get; set; }

        public decimal Rate { get => RateCount == 0 ? 0 : (TotalRate / RateCount); }
    }
}
