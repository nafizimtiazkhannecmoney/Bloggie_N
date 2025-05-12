using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly BloggieDbContext _dbContext;

        public AdminTagsController(ITagRepository tagRepository, BloggieDbContext dbContext)
        {
            this._tagRepository = tagRepository;
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // M >> D
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List", "AdminTags");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            // M >> D
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            
            var updatedTag =  await _tagRepository.UpdateAsync(tag);

            //return RedirectToAction("Edit", new { id = editTagRequest.Id });
            return RedirectToAction("List", "AdminTags");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFront(Guid id)
        {
            var deletedTag = await _tagRepository.DeleteAsync(id);
            return RedirectToAction("List", "AdminTags");
        }
    }
}

