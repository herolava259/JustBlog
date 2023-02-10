using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace FA.JustBlog.Model.User
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        public IList<SelectListItem> RolesList { get; set; }
    }
}
