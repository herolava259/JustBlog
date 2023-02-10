using FA.JustBlog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/home")]
    [Route("admin")]
    [Authorize(Roles = $"{SD.Role_Blog_Owner},{SD.Role_Contributor}")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
