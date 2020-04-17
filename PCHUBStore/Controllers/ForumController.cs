using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Services;
using PCHUBStore.View.Models.ForumViewModels;

namespace PCHUBStore.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumServices service;

        public ForumController(IForumServices service)
        {
            this.service = service;
        }

        [HttpGet("FAQ")]
        public async Task<IActionResult> Posts()
        {
            var model = new List<ForumPostViewModel>();

            var dbModel = await this.service.GetAllForumPostsAsync();

            dbModel.ForEach(x => model.Add(new ForumPostViewModel { CreationDate = x.CreatedOn.ToString("d"), PictureUrl = x.PictureUrl, Title = x.Title, Id = x.Id }));

            return View(model);
        }

        [HttpGet("FAQ/{postId}")]
        public async Task<IActionResult> Post(string postId)
        {
            var dbModel = await this.service.GetForumPostAsync(postId);

            var model = new ForumPostViewModel { CreationDate = dbModel.CreatedOn.ToString("d"), PictureUrl = dbModel.PictureUrl, Title = dbModel.Title, Content = dbModel.Content };

            return View(model);
        }
    }
}