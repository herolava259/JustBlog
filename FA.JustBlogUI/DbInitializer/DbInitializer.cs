using FA.JustBlog.Common;
using FA.JustBlog.Entity.Context;
using FA.JustBlog.Entity.Entity;
using FA.JustBlog.GUI.DbInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.UI.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JustBlogContext _db;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            JustBlogContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Blog_Owner).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Blog_Owner)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Contributor)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FullName= "Trần Minh Hùng",
                    Age = 22,
                    Address="Hà Nội"

                }, "Hung123#").GetAwaiter().GetResult();

                User user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");

                _userManager.AddToRoleAsync(user, SD.Role_Blog_Owner).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
