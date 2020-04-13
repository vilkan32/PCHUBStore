using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public class SupportForumServices : ISupportForumServices
    {
        private readonly PCHUBDbContext context;

        public SupportForumServices(PCHUBDbContext context)
        {
            this.context = context;
        }
        public async Task CreateForumPost(string title, string sanitizedContent, string picture)
        {
            await this.context.ForumPosts.AddAsync(new Data.Models.ForumPost
            {
                Content = sanitizedContent,
                PictureUrl = picture,
                Title = title,
            });

            await this.context.SaveChangesAsync();
        }

        public async Task EditForumPostAsync(EditForumPostViewModel form)
        {
            var post = await this.context.ForumPosts.FirstOrDefaultAsync(x => x.Id == form.Id);
            if(form.CurrentPictureUrl != null)
            {
                post.PictureUrl = form.CurrentPictureUrl;
            }
            post.Title = form.Title;
            post.Content = form.Content;
            post.ModificationDate = DateTime.UtcNow;

            await this.context.SaveChangesAsync();
        }
    }
}
