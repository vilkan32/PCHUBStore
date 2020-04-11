using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.ForumViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class ForumServices : IForumServices
    {
        private readonly PCHUBDbContext context;

        public ForumServices(PCHUBDbContext context)
        {
            this.context = context;
        }
        public async Task<List<ForumPost>> GetAllForumPostsAsync()
        {
            return await this.context.ForumPosts.ToListAsync();           
        }

        public async Task<ForumPost> GetForumPostAsync(string postId)
        {
            return await this.context.ForumPosts.FirstOrDefaultAsync(x => x.Id == postId);
        }
    }
}
