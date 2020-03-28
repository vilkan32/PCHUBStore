using PCHUBStore.Areas.Administration.Models.IndexPageViewModels;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminIndexPageServices
    {
        Task<bool> PageExistsAsync(string pageName);

        Task CreateIndexPageAsync(string pageName);

        Task AddIndexPageCategoryAsync(CreatePageCategoryViewModel form, string pictureUrl, string pictureName);

        Task<List<string>> GetAllPageCategoryNamesAsync();
        Task AdditemsToCategoryAsync(IndexItemsCategoryViewModel form);

        Task<List<PageCategory>> GetIndexCategoryAsync(string category);

        Task EditIndexPageCategoryAsync(EditIndexCategoryViewModel form, string previousCategory);

        Task AddBoxAsync(AddBoxViewModel form);

        Task<IEnumerable<ColorfulBox>> GetAllBoxesAsync();

        Task EditBoxesAsync(EditBoxViewModel form);

        Task UploadMainSliderPicturesAsync(List<string> urls);

        Task<List<Picture>> GetMainSliderPicturesAsync();

        Task EditMainSliderPicturesAsync(List<EditMainSliderPictures> form);
    }
}
