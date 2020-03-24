using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminProductsServices
    {
        Task CreateCategoryAsync(string categoryName);

        Task<ICollection<string>> GetAllCategoryNamesAsync();

        Task CreateProductAsync(InsertProductViewModel form);

        Task CreateLaptopFromJSONAsync(InsertJsonProductViewModel form);

        Task CreateMonitorFromJSONAsync(InsertJsonProductViewModel form);

        Task CreateKeyboardFromJSONAsync(InsertJsonProductViewModel form);

        Task CreateMouseFromJSONAsync(InsertJsonProductViewModel form);

        Task CreateComputerFromJSONAsync(InsertJsonProductViewModel form);
    }
}
