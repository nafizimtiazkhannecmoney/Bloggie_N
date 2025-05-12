using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string urlHandle)
        {
            return View();
        }
    }
}
