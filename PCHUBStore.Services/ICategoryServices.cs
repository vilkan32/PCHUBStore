using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface ICategoryServices
    {
        Task<Page> GetPageAsync(string pageName);

        Task<bool> PageAlreadyExistsAsync(string pageName);

    }
}
