using PCHUBStore.Areas.Administration.Models.FilterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminFiltersServices
    {
        Task CreateBasicFiltersAsync(InsertBasicFiltersViewModel form);

        Task<bool> BasicFiltersExistForCategoryAsync(string category);

        Task CreateFilterCategoryAsync(InserFilterCategoryViewModel form);

        Task<bool> FilterForCategoryExistsAsync(string category, string viewSub);
    }
}
