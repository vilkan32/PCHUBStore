using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminCategoryPagesServices
    {
        Task CreateCategoryPageAsync(CreateCategoryPageViewModel form);
        Task<bool> PageAlreadyExistsAsync(string pageName);

    }
}
