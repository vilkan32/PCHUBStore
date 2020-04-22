using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Services;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class ForumController : SupportController
    {
        private readonly ISupportForumServices service;
        private readonly ICloudinaryServices cloudinary;
        private readonly IForumServices userForumService;

        public ForumController(ISupportForumServices service, ICloudinaryServices cloudinary, IForumServices userForumService)
        {
            this.service = service;
            this.cloudinary = cloudinary;
            this.userForumService = userForumService;
        }
        [HttpGet]
        public IActionResult CreateForumPost()
        {
            var model = new CreateForumPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateForumPost(CreateForumPostViewModel form)
        {
            if (this.ModelState.IsValid)
            {
                var picture = await this.cloudinary.UploadPictureAsync(form.PostPicture[0], form.Title + "picture");

                var sanitizedContent = new HtmlSanitizer().Sanitize(form.Content);

                await this.service.CreateForumPost(form.Title, sanitizedContent, picture);
            }
          

            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> EditForumPosts()
        {
            var model = new List<EditForumPostsViewModel>();

            var dbModel = await this.userForumService.GetAllForumPostsAsync();

            dbModel.ForEach(x => model.Add(new EditForumPostsViewModel { CreationDate = x.CreatedOn.ToString("d"),  PostPicture = x.PictureUrl, Title = x.Title, Id = x.Id }));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditForumPost(string postId)
        {
            var model = new EditForumPostViewModel();

            var dbModel = await this.userForumService.GetForumPostAsync(postId);
            model.Id = postId;
            model.Content = dbModel.Content;
            model.Title = dbModel.Title;
            model.CurrentPictureUrl = dbModel.PictureUrl;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditForumPost(EditForumPostViewModel form)
        {
            if (this.ModelState.IsValid)
            {
                if (form.NewPicture.Count == 1)
                {
                    form.CurrentPictureUrl = await this.cloudinary.UploadPictureAsync(form.NewPicture[0], form.Title + "picture");
                } 
                
                form.Content = new HtmlSanitizer().Sanitize(form.Content);

                await this.service.EditForumPostAsync(form);
            }
            else
            {
                return this.View(form);
            }
            return this.RedirectToAction("EditForumPosts");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteForumPost(string postId)
        {
            await this.service.DeleteForumPostAsync(postId);

            return this.RedirectToAction("EditForumPosts");
        }
    }
}