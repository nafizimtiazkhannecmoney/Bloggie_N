using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
          private readonly IBlogPostRepository _blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}
