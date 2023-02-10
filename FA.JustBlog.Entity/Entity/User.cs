using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Entity.Entity
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
