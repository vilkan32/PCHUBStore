using PCHUBStore.Areas.Administration.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public interface IAdminLayoutServices
    {
        Task<AdminLayoutViewModel> GetAdminLayoutInformationAsync(string username);
    }
}
