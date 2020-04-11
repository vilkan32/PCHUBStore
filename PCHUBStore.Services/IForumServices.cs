using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.ForumViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IForumServices
    {
        Task<List<ForumPost>> GetAllForumPostsAsync();

        Task<ForumPost> GetForumPostAsync(string postId);
    }
}
