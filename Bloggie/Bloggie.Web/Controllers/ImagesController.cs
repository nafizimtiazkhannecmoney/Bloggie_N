using System.Net;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL = await _imageRepository.UploadAsync(file);

            if (imageURL == null) 
            {
                return Problem("Something Went Wrong!", null, (int)HttpStatusCode.InternalServerError); 
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
