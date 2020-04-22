using PCHUBStore.Areas.Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public interface ISupportForumServices
    {
        Task CreateForumPost(string title, string sanitizedContent, string picture);

        Task EditForumPostAsync(EditForumPostViewModel form);

        Task DeleteForumPostAsync(string id);
    }
}
